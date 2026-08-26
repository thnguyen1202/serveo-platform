using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Serveo.WebApi.Handlers.Attributes
{
    // Custom Attribute kiểm tra CSRF
    public class ValidateCsrfTokenAttribute : TypeFilterAttribute
    {
        public ValidateCsrfTokenAttribute() : base(typeof(ValidateCsrfTokenFilter)) { }

        private class ValidateCsrfTokenFilter : IAsyncActionFilter
        {
            private readonly IAntiforgery _antiforgery;

            public ValidateCsrfTokenFilter(IAntiforgery antiforgery)
            {
                _antiforgery = antiforgery;
            }

            public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
            {
                var httpContext = context.HttpContext;
                var method = httpContext.Request.Method;

                // Bỏ qua các Request đọc dữ liệu an toàn (GET, HEAD, OPTIONS, TRACE)
                if (HttpMethods.IsGet(method) || HttpMethods.IsHead(method) ||
                    HttpMethods.IsOptions(method) || HttpMethods.IsTrace(method))
                {
                    await next();
                    return;
                }

                // Xác thực Token truyền lên từ Header X-CSRF-TOKEN
                try
                {
                    await _antiforgery.ValidateRequestAsync(httpContext);
                }
                catch (AntiforgeryValidationException ex)
                {
                    //context.Result = new BadRequestObjectResult(new { message = "CSRF Token không hợp lệ hoặc bị thiếu." });
                    context.Result = new BadRequestObjectResult(new { message = ex.Message });
                    return;
                }

                await next();
            }
        }
    }
}
