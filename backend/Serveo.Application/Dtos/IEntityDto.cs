namespace Serveo.Application.Dtos
{
    public interface IEntityDto : IEntityDto<Guid> { }


    public interface IEntityDto<TPrimaryKey>
    {
        TPrimaryKey Id { get; set; }
    }
}