namespace Serveo.WebApi.Common
{
    //public sealed class CommonErrorsOperationFilter : IOperationFilter
    //{
    //    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    //    {
    //        if (!context.MethodInfo.GetCustomAttributes(true)
    //                .OfType<ProducesCommonErrorsAttribute>()
    //                .Any())
    //        {
    //            return;
    //        }

    //        Add(operation, "400");
    //        Add(operation, "401");
    //        Add(operation, "404");
    //        Add(operation, "422");
    //        Add(operation, "500");
    //    }

    //    private static void Add(OpenApiOperation operation, string code)
    //    {
    //        if (operation.Responses.ContainsKey(code))
    //            return;

    //        operation.Responses[code] = new OpenApiResponse
    //        {
    //            Description = code,
    //        //    Content =
    //        //{
    //        //    ["application/json"] = new OpenApiMediaType
    //        //    {
    //        //        Schema = new OpenApiSchema
    //        //        {
    //        //            Reference = new OpenApiReference
    //        //            {
    //        //                Type = ReferenceType.Schema,
    //        //                Id = nameof(ApiProblemDetails)
    //        //            }
    //        //        }
    //        //    }
    //        //}
    //        };
    //    }
    //}

    //public sealed class DefaultErrorsOperationFilter : IOperationFilter

    //{

    //    private static readonly Dictionary<int, string> Responses = new()

    //    {

    //        [400] = "Bad Request",

    //        [401] = "Unauthorized",

    //        [403] = "Forbidden",

    //        [404] = "Not Found",

    //        [409] = "Conflict",

    //        [422] = "Validation Error",

    //        [500] = "Internal Server Error"

    //    };

    //    public void Apply(OpenApiOperation operation, OperationFilterContext context)

    //    {

    //        var hasAttribute = context.MethodInfo

    //        .GetCustomAttributes(typeof(ProducesCommonErrorsAttribute), true)

    //        .Any();

    //        if (!hasAttribute)

    //            return;

    //        foreach (var (statusCode, description) in Responses)

    //        {

    //            var key = statusCode.ToString();

    //            if (operation.Responses.ContainsKey(key))

    //                continue;

    //            operation.Responses[key] = new OpenApiResponse

    //            {

    //                Description = description,

    //                Content = new Dictionary<string, OpenApiMediaType>

    //                {

    //                    ["application/json"] = new()

    //                    {

    //                        Schema = new OpenApiSchema

    //                        {

    //                            //Reference = new OpenApiReference

    //                            //{

    //                            //    Type = ReferenceType.Schema,

    //                            //    Id = nameof(ApiProblemDetails)

    //                            //}

    //                        }

    //                    }

    //                }

    //            };

    //        }

    //    }

    //}
}
