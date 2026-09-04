using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Scalar.AspNetCore;
using Serilog;
using Serveo.Application;
using Serveo.Application.Abstractions;
using Serveo.Application.DependencyInjection;
using Serveo.Infrastructure.Authentication.Jwt;
using Serveo.Infrastructure.Authentication.Tokens;
using Serveo.Infrastructure.Persistence.EntityFramework.Seed;
using Serveo.WebApi;
using Serveo.WebApi.DependencyInjection;
using Serveo.WebApi.Handlers.ExceptionHandler;
using System.Text.Json;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

// 1. Thêm dịch vụ Controller (cho các đường dẫn Web API)
builder.Services.AddControllers();

// Cấu hình Serilog
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();
builder.Host.UseSerilog();
//Log.Logger = new LoggerConfiguration()
//    .MinimumLevel.Information()
//    .WriteTo.Console()
//    .WriteTo.File(
//        "Logs/log-.txt",
//        rollingInterval: RollingInterval.Day,
//        retainedFileCountLimit: 30,
//        shared: true)
//    .CreateLogger();

// 1. Đăng ký Rate Limiter Policy cho CSRF
builder.Services.AddRateLimiter(options =>
{
    options.AddPolicy("CsrfRateLimit", httpContext =>
        RateLimitPartition.GetFixedWindowLimiter(
            partitionKey: httpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown",
            factory: _ => new FixedWindowRateLimiterOptions
            {
                PermitLimit = 32,                         // Tối đa 32 request
                Window = TimeSpan.FromMinutes(1),        // Trong vòng 1 phút
                QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                QueueLimit = 0
            }));

    options.AddPolicy("LoginRateLimit", httpContext =>
        RateLimitPartition.GetFixedWindowLimiter(
            httpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown",
            _ => new FixedWindowRateLimiterOptions
            {
                PermitLimit = 8,
                Window = TimeSpan.FromMinutes(1),
                QueueLimit = 0
            }));


    // Xử lý khi vượt quá giới hạn
    options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
});

// Đăng ký dịch vụ Antiforgery
builder.Services.AddAntiforgery(options =>
{
    // Đặt tên Header mà React sẽ gửi lên
    options.HeaderName = "X-CSRF-TOKEN";

    // Cookie chứa bí mật chống giả mạo của .NET
    options.Cookie.Name = "XSRF-TOKEN-COOKIE";
    options.Cookie.HttpOnly = false;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    options.Cookie.SameSite = SameSiteMode.None;
    options.Cookie.Path = "/api/auth";
});




builder.Services.AddExceptionHandler<GlobalExceptionHandler>(); // ExceptionHandler
builder.Services.AddProblemDetails(); // optional

builder.Services.AddApplicationHttpContext(); // HttpContext
builder.Services.AddApplicationDbContext(builder.Configuration); // DbContext
builder.Services.AddIdentityConfigue(); // Identity Configue
builder.Services.AddApplicationAuthentication(builder.Configuration); // Authentication
builder.Services.AddApplicationAuthorization(); // Authorization




builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
builder.Services.AddScoped<IRefreshTokenFactory, RefreshTokenFactory>();
builder.Services.AddScoped<ITokenService, TokenService>();

builder.Services.AddMediatorHandler(); // Mediator
builder.Services.AddApplicationMappingProfile(); // ApplicationMapping
builder.Services.AddWebApiMappingProfile(); // WebApiMapping
builder.Services.AddScoped<Serveo.Application.CommandMapper>(); // CommandMapper
builder.Services.AddScoped<Serveo.WebApi.Models.PayloadMapper>(); // PayloadMapper

builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.MinimumSameSitePolicy = SameSiteMode.None;
});

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
    options.AddPolicy("ServeoOps", policy =>
    {
        policy
            .WithOrigins(
                "http://localhost:5173"
            )
            //.SetIsOriginAllowed(origin =>
            //{
            //    var host = new Uri(origin).Host;

            //    // Cho phép *.serveo.app (Prod) hoặc *.serveo.test / localhost (Dev)
            //    return host == "serveo.app" || host.EndsWith(".serveo.app")
            //        || host == "serveo.test" || host.EndsWith(".serveo.test")
            //        || host == "localhost";
            //})
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
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

// Nếu chạy sau Proxy / Cloudflare / Nginx
// RemoteIpAddress có thể là IP proxy.
// Cần bật Forwarded Headers:
builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders =
        ForwardedHeaders.XForwardedFor |
        ForwardedHeaders.XForwardedProto;
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
#if DEBUG
    using var scope = app.Services.CreateScope();
    SeedDataDefault.Initialize(scope.ServiceProvider);
#endif
}

if (!app.Environment.IsProduction())
{
    app.MapOpenApi();

    // https://scalar.com/products/api-references/integrations/aspnetcore/integration
    app.MapScalarApiReference(options =>
    {
        options.SortTagsAlphabetically();
    });
}


app.MapGet("/env", (IWebHostEnvironment env) =>
{
    return env.EnvironmentName;
});


app.UseDefaultFiles(); // Cho phép ứng dụng phục vụ Default Files (như index.html)
app.UseStaticFiles(); // Cho phép ứng dụng phục vụ Static Files từ thư mục wwwroot (js, css, images,...)
app.UseRouting();

app.UseHttpsRedirection();
app.UseForwardedHeaders();

app.UseExceptionHandler(); // ExceptionHandler

//app.UseMiddleware<ExceptionMiddleware>(); // Đăng ký middleware
app.UseRateLimiter(); // Bật Middleware Rate Limiting
app.UseCors("ServeoOps");


app.UseAuthentication();
app.UseAuthorization();

// 4. Map các Controllers API (ví dụ các đường dẫn dạng /api/[controller])
app.MapControllers(); // API endpoints
//app.MapControllerRoute(name: "areas", pattern: "{area:exists}/{controller=dashboard}/{action=index}/{id?}");
//app.MapControllerRoute(name: "default", pattern: "{controller=home}/{action=index}/{id?}").WithStaticAssets();

// 5. Cấu hình SPA Fallback Routing
// Bất kỳ route nào không khớp với API Controllers sẽ được hướng về index.html của React
app.MapFallbackToFile("{*path}", "index.html");// Home SPA - default


// Cấu hình fallback theo Host / Subdomain
// Lưu ý: Thay *.serveo.test (khi dev) hoặc *.serveo.app (khi production)

// 1. Admin Subdomain
//app.MapFallbackToFile("admin/index.html")
//   .RequireHost("admin.serveo.app", "admin.serveo.test", "admin.localhost");

//// 2. Ops Subdomain
//app.MapFallbackToFile("ops/index.html")
//   .RequireHost("ops.serveo.app", "ops.serveo.test", "ops.localhost");

//// 3. QR Subdomain
//app.MapFallbackToFile("qr/index.html")
//   .RequireHost("qr.serveo.app", "qr.serveo.test", "qr.localhost");

//// 4. Default Fallback (cho serveo.app, ord
//app.MapFallbackToFile("index.html");

app.Run();
