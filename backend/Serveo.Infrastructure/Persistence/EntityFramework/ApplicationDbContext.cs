using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Serveo.Domain.Entities.Authorization;
using Serveo.Domain.Entities.Catalog;
using Serveo.Domain.Entities.Identity;
using Serveo.Domain.Entities.Ordering;
using Serveo.Domain.Entities.Tenanting;
using Serveo.Domain.Interfaces;
using Serveo.Infrastructure.Persistence.EntityFramework.Extensions;

namespace Serveo.Infrastructure.Persistence.EntityFramework
{
    /*
    0: Install-Package Microsoft.EntityFrameworkCore.Tools
    Microsoft.EntityFrameworkCore.SqlServer
    1: Add-Migration InitialCreate -OutputDir Persistence\Migrations
    2.0: Script-Migration [name]
    2: Update-Database [name]
    */
    public class ApplicationDbContext(
        DbContextOptions options,
        ISessionContext? session = null,
        bool useSnakeCase = true)
        : IdentityDbContext<User, Role, Guid, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>(options)
    {
        private readonly bool _useSnakeCase = useSnakeCase;


        #region Identity
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        #endregion

        #region Authorization
        public DbSet<AuditLog> AuditLogs { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<ApiClient> ApiClients { get; set; }
        public DbSet<ApiClientKey> ApiClientKeys { get; set; }
        //public DbSet<UserNotification> UserNotifications { get; set; }
        #endregion

        #region Tenanting
        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<Business> Businesses { get; set; }
        //public DbSet<Subscription> Subscriptions { get; set; }
        #endregion

        #region Catalog
        public DbSet<Category> Categories { get; set; }
        public DbSet<CategoryTranslation> CategoryTranslations { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<MenuTranslation> MenuTranslations { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductTranslation> ProductTranslations { get; set; }

        public DbSet<BranchCategory> BranchCategories { get; set; }
        public DbSet<BranchMenu> BranchMenus { get; set; }
        public DbSet<BranchProduct> BranchProducts { get; set; }

        #endregion

        #region Ordering
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<Table> Tables { get; set; }
        public DbSet<TableSession> TableSessions { get; set; }
        #endregion

        /*

        #region Analytics
        public DbSet<AnalyticsSnapshot> AnalyticsSnapshots { get; set; }
        #endregion

       

        #region Kitchen
        public DbSet<KitchenTicket> KitchenTickets { get; set; }
        public DbSet<KitchenTicketItem> KitchenTicketItems { get; set; }
        #endregion

        

        #region Payment
        public DbSet<BillingTransaction> BillingTransactions { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<PaymentTransaction> PaymentTransactions { get; set; }
        public DbSet<Plan> Plans { get; set; }
        #endregion

        #region 
        public DbSet<Campaign> Campaigns { get; set; }
        public DbSet<CampaignAudience> CampaignAudiences { get; set; }
        public DbSet<CampaignDelivery> CampaignDeliveries { get; set; }
        public DbSet<CashTransaction> CashTransactions { get; set; }
        public DbSet<Coupon> Coupons { get; set; }
        public DbSet<CouponUsage> CouponUsages { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerAddress> CustomerAddresses { get; set; }
        public DbSet<CustomerFeedback> CustomerFeedbacks { get; set; }
        public DbSet<CustomerMetrics> CustomerMetricses { get; set; }
        public DbSet<CustomerSegment> CustomerSegments { get; set; }
        public DbSet<CustomerTag> CustomerTags { get; set; }
        public DbSet<CustomerTagMapping> CustomerTagMappings { get; set; }
        public DbSet<CustomerVisit> CustomerVisits { get; set; }
        public DbSet<LoyaltyAccount> LoyaltyAccounts { get; set; }
        public DbSet<LoyaltyTier> LoyaltyTiers { get; set; }
        public DbSet<LoyaltyTransaction> LoyaltyTransactions { get; set; }
        public DbSet<MarketingAutomation> MarketingAutomations { get; set; }
        public DbSet<OrderEvent> OrderEvents { get; set; }
        public DbSet<OutboxMessage> OutboxMessages { get; set; }
        public DbSet<Promotion> Promotions { get; set; }
        public DbSet<Referral> Referrals { get; set; }
        public DbSet<ReferralProgram> ReferralPrograms { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Shift> Shift { get; set; }
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<StaffCall> StaffCalls { get; set; }
        public DbSet<Station> Stations { get; set; }
        public DbSet<Usage> Usages { get; set; }
        #endregion
        */

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

            //builder.ApplyGlobalFilters(this);
            //builder.ApplyUtcDateTimeConversion();

            if (_useSnakeCase || (Database.ProviderName != null &&
                Database.ProviderName.Contains("MySql", StringComparison.OrdinalIgnoreCase)))
            {
                builder.ApplySnakeCaseNamingConvention();
            }


            //builder.Entity<Booking>().HasQueryFilter(b => !b.Diner.IsDeleted);
            //builder.Entity<Category>().HasQueryFilter(b => !b.Diner.IsDeleted);
            //builder.Entity<Discount>().HasQueryFilter(b => !b.Diner.IsDeleted);
            //builder.Entity<Menu>().HasQueryFilter(b => !b.Diner.IsDeleted);
            //builder.Entity<Order>().HasQueryFilter(b => !b.Diner.IsDeleted);
            //builder.Entity<Table>().HasQueryFilter(b => !b.Diner.IsDeleted);
            //builder.Entity<Staff>().HasQueryFilter(b => !b.Diner.IsDeleted);
            //builder.Entity<Product>().HasQueryFilter(b => !b.Diner.IsDeleted);
            //builder.Entity<OrderItem>().HasQueryFilter(b => !b.Order.Diner.IsDeleted);
            //builder.Entity<MenuProduct>().HasQueryFilter(b => !b.Menu.Diner.IsDeleted);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            this.ValidateBeforeSave();
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
