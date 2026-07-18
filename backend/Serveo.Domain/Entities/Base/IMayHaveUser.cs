namespace Serveo.Domain.Entities.Base
{
    public interface IMayHaveUser
    {
        Guid? UserId { get; set; }
    }
}