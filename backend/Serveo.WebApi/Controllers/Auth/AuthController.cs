using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serveo.Application.Abstractions.Mediator;
using Serveo.Application.Features.Identity.Auth.Login;
using Serveo.Application.Features.Identity.Auth.Me;
using Serveo.Infrastructure.Authentication.ApiKey;
using Serveo.WebApi.Common;
using Serveo.WebApi.Models.Auth;

namespace Serveo.WebApi.Controllers.Auth
{
    [Authorize]
    [Route("api/auth")]
    [ApiController]
    [Tags(ApiTags.Auth)]
    public class AuthController(IMediator mediator,
        //IAccountService accountService, 
        //IJwtService jwtService, 
        //ILoginApiKeyGuard apiKeyGuard,
        IMapper mapper) : ControllerBase
    {

        protected readonly IMediator _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        //protected readonly IAccountService _accountService = accountService ?? throw new ArgumentNullException(nameof(accountService));
        //protected readonly IJwtService _jwtService = jwtService ?? throw new ArgumentNullException(nameof(jwtService));
        private readonly IMapper _mapper = mapper;

        // https://chatgpt.com/g/g-p-6a29780664648191a9f08f561a8183e2/c/6a3aa879-5c08-83ec-9d3c-3d5f6c7549a7
        //[AllowAnonymous]
        [Authorize(AuthenticationSchemes = $"{ApiKeyAuthenticationOptions.AuthenticationScheme}")]
        [HttpPost("login")]
        [ProducesResponseType<LoginResponse>(StatusCodes.Status200OK)]
        public async Task<IActionResult> Login([FromBody] LoginRequest req, CancellationToken ct)
        {
            //await apiKeyGuard.EnsureValidAsync(this.HttpContext, ct);

            var ip = HttpContext.Request.Headers["X-Forwarded-For"].FirstOrDefault()
                    ?? HttpContext.Connection.RemoteIpAddress?.ToString();

            var commandResult = await _mediator.SendAsync(new LoginCommand(req.Email, req.Password, ip), ct);

            return this.ToActionResult(commandResult, x => Ok(_mapper.Map<LoginResponse>(x)));
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh(CancellationToken ct)
        {
            return Ok();
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout(CancellationToken ct)
        {
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
    }
}
