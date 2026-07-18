using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Serveo.Domain.Entities.Authorization;
using Serveo.Domain.Entities.Identity;
using Serveo.Domain.Entities.Tenanting;
using Serveo.Domain.Interfaces;
using Serveo.Infrastructure.Persistence.EntityFramework.Extensions;
using Serveo.Infrastructure.Persistence.EntityFrameworkCore.Extensions;

namespace Serveo.Infrastructure.Persistence.EntityFramework
{
    public class EfDbContext(
        DbContextOptions options,
        ISessionContext? session = null,
        bool useSnakeCase = false) : IdentityDbContext<User, Role, Guid, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>(options)
    {
        private readonly bool _useSnakeCase = useSnakeCase;
        private readonly ISessionContext? _session = session;

        public bool IgnoreQueryFilters { get; set; }
        public int? TenantId { get; set; }

        public DbSet<AuditLog> AuditLogs { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<TenantNotification> TenantNotifications { get; set; }
        public DbSet<Translation> Translations { get; set; }
        //public DbSet<UserAccount> UserAccounts { get; set; }
        public DbSet<UserNotification> UserNotifications { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>(b =>
            {
                b.ToTable("Users");
                b.Property(u => u.SecurityStamp).HasMaxLength(128);
                b.Property(u => u.ConcurrencyStamp).HasMaxLength(128);
                b.Property(u => u.PhoneNumber).HasMaxLength(32);
            });

            builder.Entity<Role>(b =>
            {
                b.ToTable("Roles");
                b.Property(u => u.ConcurrencyStamp).HasMaxLength(128);
            });

            builder.Entity<RoleClaim>(b => { b.ToTable("RoleClaims"); b.Property(u => u.ClaimType).HasMaxLength(256); });

            builder.Entity<UserClaim>(b => { b.ToTable("UserClaims"); b.Property(u => u.ClaimType).HasMaxLength(256); });

            builder.Entity<UserLogin>(b => { b.ToTable("UserLogins"); b.Property(u => u.ProviderDisplayName).HasMaxLength(256); });

            builder.Entity<UserRole>(b => { b.ToTable("UserRoles"); });

            builder.Entity<UserToken>(b => { b.ToTable("UserTokens"); b.Property(u => u.Value).HasMaxLength(512); });

            builder.ApplyGlobalFilters(this);
            builder.ApplyUtcDateTimeConversion();

            if (_useSnakeCase || (Database.ProviderName != null && Database.ProviderName.Contains("MySql", StringComparison.OrdinalIgnoreCase)))
            {
                builder.ApplySnakeCaseNamingConvention();
            }
        }
    }
}
