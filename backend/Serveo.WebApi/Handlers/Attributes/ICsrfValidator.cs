namespace Serveo.WebApi.Handlers.Attributes
{
    public interface ICsrfValidator
    {
        void Validate(HttpContext context);
    }
}
