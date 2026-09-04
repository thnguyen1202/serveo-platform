using Serveo.Domain.Entities.Ordering;
using System.ComponentModel.DataAnnotations;

namespace Serveo.WebApi.Models.Ordering.Tables
{
    public class CreateTableResponse
    {
        public Guid Id { get; set; }
        public string Code { get; set; } = default!;
        public string Name { get; set; } = default!;
        public int Capacity { get; set; }
        public string PublicToken { get; set; } = default!;
        public TableStatus Status { get; set; }
    }
}
