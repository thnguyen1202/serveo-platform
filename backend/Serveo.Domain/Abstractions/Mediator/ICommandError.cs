namespace Serveo.Domain.Abstractions.Mediator
{
    public interface ICommandError
    {
        public string Code { get; set; }
        public string Description { get; set; }
    }
}
