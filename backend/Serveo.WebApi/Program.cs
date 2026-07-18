using Microsoft.AspNetCore.Mvc;
using Scalar.AspNetCore;
using Serveo.Application;
using Serveo.Application.DependencyInjection;
using Serveo.Infrastructure.Persistence.EntityFramework.Seed;
using Serveo.WebApi;
using Serveo.WebApi.DependencyInjection;
using Serveo.WebApi.Handlers.ExceptionHandler;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);




builder.Services.AddExceptionHandler<GlobalExceptionHandler>(); // ExceptionHandler
builder.Services.AddProblemDetails(); // optional

builder.Services.AddApplicationHttpContext(); // HttpContext
builder.Services.AddApplicationDbContext(builder.Configuration); // DbContext
builder.Services.AddIdentityConfigue(); // Identity Configue
builder.Services.AddApplicationAuthentication(builder.Configuration); // Authentication
builder.Services.AddApplicationAuthorization(); // Authorization

builder.Services.AddMediatorHandler(); // Mediator
builder.Services.AddApplicationMappingProfile(); // ApplicationMapping
builder.Services.AddWebApiMappingProfile(); // WebApiMapping

builder.Services.AddCors(options =>
{
    options.AddPolicy("AdminCors", policy =>
    {
        policy
            .WithOrigins(
                "http://localhost:5173"
            )
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

//var jwt = builder.Configuration.GetSection("Jwt").Get<JwtOptions>();

//builder.Services
//    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//    .AddJwtBearer(options =>
//    {
//        options.TokenValidationParameters = new TokenValidationParameters
//        {
//            ValidateIssuer = true,
//            ValidateAudience = true,
//            ValidateLifetime = true,
//            ValidateIssuerSigningKey = true,

//            ValidIssuer = jwt!.Issuer,
//            ValidAudience = jwt.Audience,
//            IssuerSigningKey = new SymmetricSecurityKey(
//                Encoding.UTF8.GetBytes(jwt.Key)
//            ),

//            ClockSkew = TimeSpan.Zero // quan trọng: không delay expire
//        };
//    });


// JWT cho SignalR:
//options.Events = new JwtBearerEvents
//{
//    OnMessageReceived = context =>
//    {
//        var accessToken =
//            context.Request.Query["access_token"];

//        if (!string.IsNullOrEmpty(accessToken))
//        {
//            context.Token = accessToken;
//        }

//        return Task.CompletedTask;
//    }
//};

// Add services to the container.
//builder.Services.AddControllers()
builder.Services.AddControllersWithViews()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(
            new System.Text.Json.Serialization.JsonStringEnumConverter());
    });

builder.Services.AddProblemDetails(options =>
{
    options.CustomizeProblemDetails = context =>
    {
        if (context.ProblemDetails is ValidationProblemDetails validation)
        {
            var errors = validation.Errors
                .ToDictionary(
                    x => JsonNamingPolicy.CamelCase.ConvertName(x.Key),
                    x => x.Value);

            validation.Errors.Clear();

            foreach (var error in errors)
            {
                validation.Errors.Add(
                    error.Key,
                    error.Value);
            }
        }
    };
});

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
//builder.Services.AddOpenApi(options =>
//{
//    options.AddOperationTransformer((operation, context, ct) =>
//    {
//        if (operation.RequestBody?.Content?.TryGetValue(
//                "application/json",
//                out var mediaType) == true)
//        {
//            if (mediaType is OpenApiMediaType openApiMediaType)
//            {
//                openApiMediaType.Example = new JsonObject
//                {
//                    ["type"] = "https://example.com/errors/validation",
//                    ["title"] = "Validation error",
//                    ["status"] = 400,
//                    ["detail"] = "One or more validation errors occurred."
//                };
//            }
//        }

//        return Task.CompletedTask;
//    });
//});
//builder.Services.AddOpenApi(options =>
//{
//options.AddOperationTransformer();
//var responses = new Dictionary<int, string>()
//{
//    //[400] = "Bad Request",
//    [401] = "Unauthorized",
//    [403] = "Forbidden",
//    [404] = "Not Found",
//    [409] = "Conflict",
//    [422] = "Validation Error",
//    [500] = "Internal Server Error"
//};


//options.AddOperationTransformer(async (operation, context, cancellationToken) =>
//{
//    if (operation.Responses is null) return;

//    if (!operation.Responses!.ContainsKey("400"))
//    {
//        operation.Responses?.TryAdd("400", new OpenApiResponse
//        {
//            Description = "Bad Request",
//            Content = new Dictionary<string, OpenApiMediaType>
//            {
//                ["application/json"] = new OpenApiMediaType
//                {
//                    Schema = await context.GetOrCreateSchemaAsync(typeof(ValidationProblemDetails), cancellationToken: cancellationToken)
//                }
//            }
//        });
//    }

//    foreach (var (statusCode, description) in responses)
//    {
//        var key = statusCode.ToString();
//        if (operation.Responses!.ContainsKey(key)) continue;

//        operation.Responses?.TryAdd(key, new OpenApiResponse
//        {
//            Description = description,
//            Content = new Dictionary<string, OpenApiMediaType>
//            {
//                ["application/json"] = new OpenApiMediaType
//                {
//                    Schema = await context.GetOrCreateSchemaAsync(typeof(ApiProblemDetails), cancellationToken: cancellationToken)
//                }
//            }
//        });
//    }
//});
//});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    // https://scalar.com/products/api-references/integrations/aspnetcore/integration
    app.MapScalarApiReference(options =>
    {
        options.SortTagsAlphabetically();
    });

#if DEBUG
    using var scope = app.Services.CreateScope();
    SeedDataDefault.Initialize(scope.ServiceProvider);
#endif
}


app.UseHttpsRedirection();
app.UseForwardedHeaders();

app.UseExceptionHandler(); // ExceptionHandler

//app.UseMiddleware<ExceptionMiddleware>(); // Đăng ký middleware

app.UseCors("AdminCors");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers(); // API endpoints
app.MapControllerRoute(name: "areas", pattern: "{area:exists}/{controller=dashboard}/{action=index}/{id?}");
app.MapControllerRoute(name: "default", pattern: "{controller=home}/{action=index}/{id?}").WithStaticAssets();

app.Run();
