using Serveo.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace Serveo.WebApi.Models
{
    public class EntityModel : EntityModel<int>
    {
    }

    public class EntityModel<TPrimaryKey> : IEntity<TPrimaryKey>
    {
        [Required]
        public virtual TPrimaryKey Id { get; set; } = default!;
    }
}
