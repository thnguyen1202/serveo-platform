using Microsoft.AspNetCore.Identity;
using Serveo.Application.Abstractions.Messaging;
using Serveo.Domain.Entities.Identity;
using Serveo.Domain.Interfaces;

namespace Serveo.Application.Services.Administration.Accounts
{
    public class AccountService(
        IUnitOfWork unitOfWork,
        UserManager<User> userManager,
        SignInManager<User> signInManager) : IAccountService, IService
    {
        //private readonly ILogger _logger;
        private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        private readonly UserManager<User> _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        private readonly SignInManager<User> _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));

        //public async Task<CommandResult<User>> RegisterAsync(RegisterDto input)
        //{
        //    if (await IsDuplicateUserNameAsync(input.Username))
        //        return CommandResult<User>.Failed(new CommandError { Description = $"{input.Username} already exists." });

        //    if (await IsDuplicateEmailAsync(input.Email))
        //        return CommandResult<User>.Failed(new CommandError { Description = $"{input.Email} already exists." });

        //    var entity = new User
        //    {
        //        UserName = input.Username,
        //        Email = input.Email,
        //        EmailConfirmed = true,
        //        TimeZoneId = TimeZoneInfo.Local.Id
        //    };

        //    var identityResult = await _userManager.CreateAsync(entity, input.Password);
        //    if (!identityResult.Succeeded)
        //        return CommandResult<User>.Failed(identityResult.Errors);

        //    var result = CommandResult<User>.Success;
        //    result.Data = entity;

        //    return result;
        //}

        public async Task<ICommandResult<User>> LoginAsync(string emailOrUsername, string password)
        {
            var user = await _userManager.FindByEmailAsync(emailOrUsername);
            user ??= await _userManager.FindByNameAsync(emailOrUsername);

            if (user is null)
                return (ICommandResult<User>)CommandResult.Failure(new CommandError { Message = "User not confirmed yet." });

            var lockoutOnFailure = true;
            var signinResult = await _signInManager.CheckPasswordSignInAsync(user, password, lockoutOnFailure);
            if (signinResult.Succeeded)
            {
                return CommandResult<User>.Success(user);
            }
            if (signinResult.IsLockedOut)
            {
                return (ICommandResult<User>)CommandResult.Failure(new CommandError { Message = "User account locked out." });
            }
            else
            {
                var errors = new List<CommandError>()
                {
                    new() { Message = "Invalid login attempt." }
                };
                string remaining = "";
                if (lockoutOnFailure)
                {
                    var attemptsLeft = _userManager.Options.Lockout.MaxFailedAccessAttempts - await _userManager.GetAccessFailedCountAsync(user);
                    remaining = $"Remaining attempts : {attemptsLeft}.";
                }
                if (!string.IsNullOrWhiteSpace(remaining))
                {
                    errors.Add(new() { Message = remaining });
                }

                return (ICommandResult<User>)CommandResult.Failure(errors);
            }
        }

        //public async Task<CommandResult> LoginAsync(string refreshToken)
        //{
        //    var user = await _userManager.Users.FirstOrDefaultAsync(x => x.RefreshToken == refreshToken);

        //    if (user == null)
        //        return CommandResult.Failed(new CommandError { Description = "Invalid refresh token." });

        //    if (user.TokenExpiryTime < DateTime.UtcNow)
        //        return CommandResult.Failed(new CommandError { Description = "Your refresh token has expired. Please log in again." });

        //    return CommandResult.Success;
        //}

        //public async Task<IssueJwtResult?> IssueJwtAsync(string token, bool isRefreshToken = false)
        //{
        //    var user = isRefreshToken ?
        //        await _userManager.Users.FirstOrDefaultAsync(x => x.RefreshToken == token) :
        //        await _userManager.FindByEmailAsync(token);

        //    if (!isRefreshToken && user == null)
        //        user = await _userManager.FindByNameAsync(token);

        //    if (user is null) return null;

        //    return await IssueJwtAsync(user);
        //}

        //public async Task<IssueJwtResult?> IssueJwtAsync(User user)
        //{
        //    if (user is null) return null;

        //    var claims = (await _signInManager.CreateUserPrincipalAsync(user)).Claims.Where(x => x.Type != "Permission").ToList();
        //    if (!string.IsNullOrEmpty(user.DisplayName)) claims.Add(new System.Security.Claims.Claim("DisplayName", user.DisplayName));
        //    if (!string.IsNullOrEmpty(user.TimeZoneId)) claims.Add(new System.Security.Claims.Claim("TimeZoneId", user.TimeZoneId));
        //    if (user.TenantId.HasValue)
        //    {
        //        claims.Add(new System.Security.Claims.Claim("TenantId", user.TenantId.Value.ToString()));

        //        var restaurant = await _restaurantRepository.FindAsync(x => x.TenantId == user.TenantId);
        //        if (restaurant != null)
        //        {
        //            claims.Add(new System.Security.Claims.Claim("RestaurantId", restaurant.Id.ToString()));
        //        }
        //    }

        //    var expires = DateTime.UtcNow.AddMinutes(_settings.Value.TokenLifetimeMinutes);
        //    var accessToken = JwtTokenHelper.GenerateJwtToken(_settings.Value.Issuer, _settings.Value.Audience, claims, expires, _settings.Value.SecretKey);
        //    var refreshToken = JwtTokenHelper.GenerateRefreshToken();

        //    user.RefreshToken = refreshToken;
        //    user.TokenExpiryTime = expires;
        //    await _userManager.UpdateAsync(user);

        //    return new IssueJwtResult(accessToken, refreshToken, _settings.Value.TokenLifetimeMinutes);
        //}

        public async Task<bool> IsDuplicateUserNameAsync(string username, Guid? id = null)
        {
            if (string.IsNullOrWhiteSpace(username)) return false;

            var entity = await _userManager.FindByNameAsync(username);
            if (entity != null && entity.Id != id)
                return true;

            return false;
        }

        public async Task<bool> IsDuplicateEmailAsync(string email, Guid? id = null)
        {
            if (string.IsNullOrWhiteSpace(email)) return false;

            var entity = await _userManager.FindByEmailAsync(email);
            if (entity != null && entity.Id != id)
                return true;

            return false;
        }
    }
}
