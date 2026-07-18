using Microsoft.AspNetCore.Identity;
using Serveo.Domain.Entities.Base;

namespace Serveo.Domain.Entities.Identity
{
    public sealed class Role : IdentityRole<Guid>, IHasCreatedAt, IMayHaveTenant
    {
        public Guid? TenantId { get; set; }

        /// <summary>
        /// Is this a static role? Static roles can not be deleted, can not change their
        ///  name. They can be used programmatically.
        /// </summary>
        public bool IsStatic { get; set; }

        /// <summary>
        /// Is this role will be assigned to new users as default?
        /// </summary>
        public bool IsDefault { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

        public void SetNormalizedNames()
        {
            NormalizedName = Name?.ToUpperInvariant();
        }
    }
}