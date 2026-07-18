using Serveo.Domain.Entities.Base;

namespace Serveo.WebApi.Models
{
    public class UpdateSatusModel : IPassivable
    {
        public bool IsActive { get; set; }
    }
}
