using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Serveo.Application.Abstractions;
using Serveo.Application.Abstractions.Mediator;
using Serveo.Domain.Entities.Identity;
using Serveo.Domain.Entities.Tenanting;

namespace Serveo.Application.Features.Tenanting.RegisterTenant
{
    public class RegisterTenantHandler(
        IUnitOfWork unitOfWork,
        //IMapper mapper,
        IOptions<IdentityOptions> identityOptions
    ) : ICommandHandler<RegisterTenantCommand, RegisterTenantResult>
    {
        private readonly IdentityOptions _identityOptions = identityOptions.Value;
        public async Task<RegisterTenantResult> HandleAsync(RegisterTenantCommand request, CancellationToken ct)
        {
            var value = await unitOfWork.ExecuteAsync(async () =>
            {
                // create tenant
                var tenant = new Serveo.Domain.Entities.Tenanting.Tenant
                {
                    Name = request.TenantName,
                    Code = request.TenantName,
                };
                unitOfWork.Add(tenant);

                // create business
                var business = new Business
                {
                    TenantId = tenant.Id,
                    Name = request.TenantName,
                    Address = request.Address,
                    Phone = request.Phone,
                };
                unitOfWork.Add(business);

                var branch = new Branch
                {
                    BusinessId = business.Id,
                    Name = request.BranchName ?? request.TenantName,
                    Address = request.Address,
                };
                unitOfWork.Add(branch);

                var hasher = new PasswordHasher<User>();
                var user = new User
                {
                    TenantId = tenant.Id,
                    BusinessId = business.Id,
                    BranchId = branch.Id,
                    UserName = request.Email,
                    Email = request.Email,
                    DisplayName = request.OwnerName,
                    PhoneNumber = request.Phone,
                    EmailConfirmed = !_identityOptions.SignIn.RequireConfirmedEmail,
                    TimeZoneId = request.TimeZone ?? TimeZoneInfo.Local.Id
                };
                user.SetNormalizedNames();
                user.SetSecurityStamp();
                user.PasswordHash = hasher.HashPassword(user, request.Password);
                unitOfWork.Add(user);

                return new RegisterTenantResult(
                    tenant.Id,
                    branch.Id,
                    user.Id,
                    !_identityOptions.SignIn.RequireConfirmedEmail
                );
            }, ct);

            return value;
        }

        public async Task<bool> IsDuplicateEmailAsync(string email, Guid? id = null)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            email = email.Trim();

            return await unitOfWork.Users.Query()
                .AnyAsync(x =>
                    x.NormalizedEmail == email.ToUpperInvariant().Trim()
                    && (!id.HasValue || x.Id != id.Value));
        }
    }
}
