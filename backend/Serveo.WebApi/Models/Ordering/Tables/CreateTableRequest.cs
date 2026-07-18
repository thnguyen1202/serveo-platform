using System.ComponentModel.DataAnnotations;

namespace Serveo.WebApi.Models.Ordering.Tables
{
    public class CreateTableRequest
    {
        [Required]
        [StringLength(64)]
        public string Name { get; set; } = default!;

        public int Capacity { get; set; } = 2;

        public Guid BranchId { get; set; }
    }
}
