using System.Security.Cryptography;
using System.Text;

namespace Serveo.WebApi.Handlers.Attributes
{
    public sealed class CsrfValidator : ICsrfValidator
    {
        public void Validate(HttpContext context)
        {
            var cookieToken =
                context.Request.Cookies["csrf_token"];

            var headerToken =
                context.Request.Headers["X-CSRF-TOKEN"].FirstOrDefault();

            if (string.IsNullOrWhiteSpace(cookieToken) ||
                string.IsNullOrWhiteSpace(headerToken) ||
                !CryptographicOperations.FixedTimeEquals(
                    Encoding.UTF8.GetBytes(cookieToken),
                    Encoding.UTF8.GetBytes(headerToken)))
            {
                //throw new CsrfValidationException();
            }
        }
    }
}
