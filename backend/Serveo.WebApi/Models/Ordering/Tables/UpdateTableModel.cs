using System.ComponentModel.DataAnnotations;

namespace Serveo.WebApi.Models.Ordering.Tables
{
    public class UpdateTableModel : CreateTableRequest
    {
        [Required]
        public string Id { get; set; } = default!;
    }
}
