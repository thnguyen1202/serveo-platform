using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Serveo.Application.Abstractions.Mediator;
using Serveo.Application.Features.Identity.Auth.Login;
using Serveo.Application.Features.Identity.Auth.Me;
using Serveo.Application.Features.Identity.Auth.RefreshToken;
using Serveo.Infrastructure.Authentication.ApiKey;
using Serveo.WebApi.Common;
using Serveo.WebApi.Handlers.Attributes;
using Serveo.WebApi.Models;
using Serveo.WebApi.Models.Auth;

namespace Serveo.WebApi.Controllers.Auth
{
    [Route("api/auth")]
    [ApiController]
    [Tags(ApiTags.Auth)]
    public class AuthController(
        IMediator mediator,
        IAntiforgery antiforgery,
        PayloadMapper mapper) : ControllerBase
    {
        // https://chatgpt.com/g/g-p-6a29780664648191a9f08f561a8183e2/c/6a3aa879-5c08-83ec-9d3c-3d5f6c7549a7
        [Authorize(AuthenticationSchemes = $"{ApiKeyAuthenticationOptions.AuthenticationScheme}")]
        [HttpPost("login")]
        [ProducesResponseType<LoginResponse>(StatusCodes.Status200OK)]
        public async Task<IActionResult> Login([FromBody] LoginRequest request, CancellationToken ct)
        {
            var ip = HttpContext.Request.Headers["X-Forwarded-For"].FirstOrDefault()
                    ?? HttpContext.Connection.RemoteIpAddress?.ToString();
            var userAgent = HttpContext.Request.Headers.UserAgent.ToString();

            var command = new LoginCommand(
                request.Email,
                request.Password,
                request.IsRemember,
                request.ClientType,
                request.DeviceId,
                ip ?? "",
                userAgent ?? ""
                );

            var commandResult = await mediator.SendAsync(command, ct);
            if (commandResult.Succeeded)
            {
                SetRefreshTokenCookie(commandResult.Value.RefreshToken, request.IsRemember);
            }

            return this.ToActionResult(commandResult, x => Ok(mapper.ToResponse(x)));
        }


        [Authorize(AuthenticationSchemes = $"{ApiKeyAuthenticationOptions.AuthenticationScheme}")]
        [ValidateCsrfToken]
        [HttpPost("refresh")]
        [ProducesResponseType<RefreshTokenResponse>(StatusCodes.Status200OK)]
        public async Task<IActionResult> Refresh([FromBody] RefreshTokenRequest request, CancellationToken ct)
        {
            var refreshToken = Request.Cookies[CookieNames.RefreshToken];

            var ip = HttpContext.Request.Headers["X-Forwarded-For"].FirstOrDefault()
                    ?? HttpContext.Connection.RemoteIpAddress?.ToString();
            var userAgent = HttpContext.Request.Headers.UserAgent.ToString();

            var command = new RefreshTokenCommand(
                refreshToken ?? "",
                request.ClientType,
                request.DeviceId,
                ip ?? "",
                userAgent
                );

            var commandResult = await mediator.SendAsync(command, ct);
            if (commandResult.Succeeded)
            {
                //antiforgery.GetAndStoreTokens(HttpContext);
                SetRefreshTokenCookie(commandResult.Value.RefreshToken, commandResult.Value.IsRemember);
            }

            return this.ToActionResult(commandResult, x => Ok(mapper.ToResponse(x)));
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout(CancellationToken ct)
        {
            // validate CSRF

            // revoke current session

            // clear refresh token cookie
            //Response.Cookies.Delete(
            //    CookieNames.RefreshToken,
            //    new CookieOptions
            //    {
            //        Path = "/api/auth",
            //        Secure = true,
            //        SameSite = SameSiteMode.None
            //    });

            //Response.Cookies.Delete(
            //    CookieNames.CsrfToken,
            //    new CookieOptions
            //    {
            //        Path = "/",
            //        Secure = true,
            //        SameSite = SameSiteMode.None
            //    });

            //return NoContent();

            return Ok();
        }

        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword(CancellationToken ct)
        {
            return Ok();
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(CancellationToken ct)
        {
            return Ok();
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(CancellationToken ct)
        {
            return Ok();
        }

        [HttpGet("me")]
        public async Task<IActionResult> Me(CancellationToken ct)
        {
            var result = await mediator.SendAsync(new MeCommand(), ct);

            return this.ToActionResult(result);
        }

        [Authorize(AuthenticationSchemes = ApiKeyAuthenticationOptions.AuthenticationScheme)]
        [HttpGet("csrf")]
        [EnableRateLimiting("CsrfRateLimit")] // Áp dụng giới hạn cho API
        public IActionResult GetCsrfToken()
        {
            var tokens = antiforgery.GetAndStoreTokens(HttpContext);

            return Ok(new { csrfToken = tokens.RequestToken });
        }

        //[AllowAnonymous]
        //[HttpPost("login")]
        //[ProducesResponseType<LoginResponse>(StatusCodes.Status200OK)]
        //public async Task<ActionResult<ApiResponse<LoginResponse>>> Login([FromBody] LoginRequest req, CancellationToken ct)
        //{
        //    var result = await _accountService.LoginAsync(req.Email, req.Password);
        //    if (!result.Succeeded)
        //    {
        //        return BadRequest(ApiResponse.Fail(result.Errors.Select(s => s.Message), "Bad Request"));
        //    }
        //    else
        //    {
        //        var ip = HttpContext.Request.Headers["X-Forwarded-For"].FirstOrDefault()
        //            ?? HttpContext.Connection.RemoteIpAddress?.ToString();
        //        var authToken = await _jwtService.AuthTokenAsync(result.Value!, ip);
        //        var data = _mapper.Map<AuthTokenResult>(authToken);

        //        return Ok(ApiResponse<AuthTokenResult>.Ok(data));
        //    }
        //}

        //    public async Task<TokenResponse> RefreshAsync(
        //string refreshToken)
        //    {
        //        var token = await _db.RefreshTokens
        //            .SingleOrDefaultAsync(x =>
        //                x.Token == refreshToken);

        //        if (token == null)
        //            throw new UnauthorizedException();

        //        if (!token.IsActive)
        //            throw new UnauthorizedException();

        //        var user = await _db.Users
        //            .FindAsync(token.UserId);

        //        var newAccessToken =
        //            _jwtService.GenerateAccessToken(user);

        //        var newRefreshToken =
        //            GenerateRefreshToken();

        //        token.RevokedAt =
        //            DateTime.UtcNow;

        //        token.ReplacedByToken =
        //            newRefreshToken;

        //        _db.RefreshTokens.Add(
        //            new RefreshToken
        //            {
        //                UserId = user.Id,
        //                Token = newRefreshToken,
        //                CreatedAt = DateTime.UtcNow,
        //                ExpiresAt = DateTime.UtcNow.AddDays(30)
        //            });

        //        await _db.SaveChangesAsync();

        //        return new TokenResponse
        //        {
        //            AccessToken = newAccessToken,
        //            RefreshToken = newRefreshToken
        //        };
        //    }

        #region Helper Methods (Quản lý Cookie)

        private void SetRefreshTokenCookie(string refreshToken, bool isRemeber = false)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true, // Bắt buộc dùng HTTPS ở môi trường Staging/Production
                //SameSite = SameSiteMode.Strict,
                SameSite = SameSiteMode.None,       // Cho phép gửi Cookie Cross-Site
                Path = "/api/auth/refresh", // Giới hạn Cookie chỉ gửi kèm các request truy cập /api/auth
                MaxAge = isRemeber
                    ? TimeSpan.FromDays(90)// 90 ngày
                    : null
            };

            Response.Cookies.Append(CookieNames.RefreshToken, refreshToken, cookieOptions);
        }

        private void SetCsrfTokenCookie(string csrfToken)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = false,
                Secure = true, // Bắt buộc dùng HTTPS ở môi trường Staging/Production
                SameSite = SameSiteMode.None,
                Path = "/", // Giới hạn Cookie chỉ gửi kèm các request truy cập /api/auth
                MaxAge = TimeSpan.FromMinutes(30)
            };

            Response.Cookies.Append(CookieNames.CsrfToken, csrfToken, cookieOptions);
        }

        private void DeleteRefreshTokenCookie()
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Path = "/api/auth"
            };

            // Hàm Delete cần các thuộc tính (Path, Secure, SameSite) khớp hoàn toàn với lúc Append
            Response.Cookies.Delete("refreshToken", cookieOptions);
        }

        #endregion
    }
}
