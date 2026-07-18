namespace Serveo.Domain.Exceptions
{
    public class BusinessException(string message, string code = "business_error") : Exception(message)
    {
        public string Code { get; } = code;
    }
}
