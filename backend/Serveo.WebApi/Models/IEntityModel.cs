namespace Serveo.WebApi.Models
{
    public interface IEntityModel : IEntityModel<int>
    {
    }

    public interface IEntityModel<TPrimaryKey>
    {
        TPrimaryKey Id { get; set; }
    }
}
