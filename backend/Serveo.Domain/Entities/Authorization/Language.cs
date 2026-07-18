using Serveo.Domain.Entities.Base;

namespace Serveo.Domain.Entities.Authorization
{
    public sealed class Language : Entity, IPassivable
    {
        public string Code { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string? NativeName { get; set; }

        /// <summary>
        /// Is this a static role? Static language can not be deleted, can not change their name. They can be used programmatically.
        /// </summary>
        public bool IsStatic { get; set; }

        /// <summary>
        /// Is this language the default language?
        /// </summary>
        public bool IsDefault { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
