using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Serveo.Application.Abstractions;
using Serveo.Application.Common.Results;
using Serveo.Infrastructure.Authentication.ApiKey;
using Serveo.Infrastructure.Authentication.Jwt;
using Serveo.WebApi.Common;
using Serveo.WebApi.Handlers.AuthenticationHandlers;
using System.Diagnostics;
using System.Text;

namespace Serveo.WebApi.DependencyInjection
{
    public static class ApplicationAuthenticationServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            // bind config
            services
                .AddOptions<JwtOptions>()
                .BindConfiguration(JwtOptions.SectionName)
                .ValidateDataAnnotations()
                .ValidateOnStart();

            services
                .AddOptions<ApiKeyOptions>()
                .BindConfiguration(ApiKeyOptions.SectionName)
                .ValidateDataAnnotations()
                .ValidateOnStart();

            services.AddScoped<IApiKeyValidator, ApiKeyValidator>(); //
            //services.AddScoped<ILoginApiKeyGuard, LoginApiKeyGuard>(); //

            // Configure cookie for Identity
            services.ConfigureApplicationCookie(options =>
            {
                options.AccessDeniedPath = "/identity/account/accessdenied";
                options.Cookie.Name = "dinersaas.admin.cms";
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromDays(7);
                options.LoginPath = "/identity/account/login";

                options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
                options.SlidingExpiration = true;
            });

            // Authentication
            services.AddAuthentication(options =>
            {
                // default Cookie of Identity
                //options.DefaultAuthenticateScheme = IdentityConstants.ApplicationScheme;
                //options.DefaultChallengeScheme = IdentityConstants.ApplicationScheme;
                //options.DefaultSignInScheme = IdentityConstants.ApplicationScheme;

                // default jwt
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            // Basic Auth
            .AddScheme<BasicAuthenticationOptions, BasicAuthenticationHandler>(BasicAuthenticationOptions.AuthenticationScheme, _ => { })
            // API Key
            .AddScheme<ApiKeyAuthenticationOptions, ApiKeyAuthenticationHandler>(ApiKeyAuthenticationOptions.AuthenticationScheme, options => { })
            // JWT Bearer (API)
            .AddJwtBearer(options =>
            {
                var jwt = configuration.GetSection(JwtOptions.SectionName).Get<JwtOptions>()
                    ?? throw new InvalidOperationException("Jwt configuration is missing.");

                options.RequireHttpsMetadata = true; // dev -> false
                options.SaveToken = true;

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = jwt.Issuer,

                    ValidateAudience = true,
                    ValidAudience = jwt.Audience,

                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromMinutes(1), //avoid time discrepancies // dev -> TimeSpan.Zero 

                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.SigningKey))
                };

                options.Events = new JwtBearerEvents
                {
                    OnChallenge = async context =>
                    {
                        // 1. Skip the default logic since we are handling it
                        context.HandleResponse();

                        if (context.Response.HasStarted) return;
                        context.Response.Clear();

                        var statusCode = StatusCodes.Status401Unauthorized;

                        // 2. Set the response metadata FIRST
                        context.Response.StatusCode = statusCode;
                        context.Response.ContentType = "application/json";

                        var problem = new ApiProblemDetails
                        {
                            Type = ProblemTypeCatalog.FromStatusCode(statusCode),
                            Title = ProblemTitleCatalog.FromStatusCode(statusCode),
                            Status = statusCode,
                            Detail = "Authentication is required.",
                            Instance = $"{context.Request.Method} {context.Request.Path}",
                            TraceId = Activity.Current?.Id,
                            Errors =
                            [
                                new() {Code = ErrorCodes.Auth.InvalidAccessToken, Message = "Invalid token or token expired."}
                            ]
                        };

                        // 3. Write to the body LAST (this starts/freezes the response)
                        await context.Response.WriteAsJsonAsync(problem);
                    },

                    OnForbidden = async context =>
                    {
                        var statusCode = StatusCodes.Status403Forbidden;
                        var problem = new ApiProblemDetails
                        {
                            Type = ProblemTypeCatalog.FromStatusCode(statusCode),
                            Title = ProblemTitleCatalog.FromStatusCode(statusCode),
                            Status = statusCode,
                            Detail = "Access denied.",
                            Instance = $"{context.Request.Method} {context.Request.Path}",
                            TraceId = Activity.Current?.Id,
                            Errors =
                            [
                                new() {Code = ErrorCodes.Auth.PermissionDenied, Message = "You don’t have access."}
                            ]
                        };

                        context.Response.StatusCode = statusCode;
                        context.Response.ContentType = "application/json";

                        await context.Response.WriteAsJsonAsync(problem);
                    },

                    OnAuthenticationFailed = async context =>
                    {
                        var statusCode = StatusCodes.Status401Unauthorized;
                        var problem = new ApiProblemDetails
                        {
                            Type = ProblemTypeCatalog.FromStatusCode(statusCode),
                            Title = ProblemTitleCatalog.FromStatusCode(statusCode),
                            Status = statusCode,
                            Detail = "Authentication failed.",
                            Instance = $"{context.Request.Method} {context.Request.Path}",
                            TraceId = Activity.Current?.Id,
                            Errors =
                            [
                                new() {Code = ErrorCodes.Auth.PermissionDenied, Message = "Permission denied."}
                            ]
                        };

                        context.Response.StatusCode = statusCode;
                        context.Response.ContentType = "application/json";

                        await context.Response.WriteAsJsonAsync(problem);
                    }
                };
            });

            // 4. Authorization: policy-based để mix nhiều scheme
            //services.AddAuthorization(options =>
            //{
            //    // Policy cho API JWT + ApiKey
            //    options.AddPolicy("ApiPolicy", policy =>
            //        policy.AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme,
            //                                        ApiKeyAuthOptions.DefaultScheme)
            //              .RequireAuthenticatedUser());

            //    // Policy cho Basic + JWT + ApiKey (nếu cần)
            //    options.AddPolicy("ApiBasicPolicy", policy =>
            //        policy.AddAuthenticationSchemes(BasicAuthOptions.DefaultScheme,
            //                                        JwtBearerDefaults.AuthenticationScheme,
            //                                        ApiKeyAuthOptions.DefaultScheme)
            //              .RequireAuthenticatedUser());

            //    // Policy cho Web Admin
            //    options.AddPolicy("WebAdminOnly", policy =>
            //        policy.RequireRole("Admin"));
            //});

            return services;
        }

        //public static IServiceCollection AddCmsAuthentication(this IServiceCollection services)
        //{
        //    services.ConfigureApplicationCookie(options =>
        //    {
        //        options.AccessDeniedPath = "/identity/account/accessdenied";
        //        options.Cookie.Name = "dinersaas.admin.cms";
        //        options.Cookie.HttpOnly = true;
        //        options.ExpireTimeSpan = TimeSpan.FromDays(7);
        //        options.LoginPath = "/identity/account/login";

        //        options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
        //        options.SlidingExpiration = true;
        //    });

        //    return services;
        //}

        //public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        //{
        //    var jwtSection = configuration.GetSection("Authentication:Schemes:Jwt");
        //    var secretKey = jwtSection["SecretKey"]!;
        //    services
        //        .AddAuthentication(options =>
        //        {
        //            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        //            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        //        })
        //        .AddJwtBearer(options =>
        //        {
        //            options.RequireHttpsMetadata = false; // dev only
        //            options.SaveToken = true; // get token in HttpContext.GetTokenAsync("access_token")

        //            options.TokenValidationParameters = new TokenValidationParameters
        //            {
        //                ValidateIssuer = true,
        //                ValidIssuer = jwtSection["Issuer"],

        //                ValidateAudience = true,
        //                ValidAudience = jwtSection["Audience"],

        //                ValidateLifetime = true,
        //                ClockSkew = TimeSpan.Zero,

        //                ValidateIssuerSigningKey = true,
        //                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
        //            };

        //            options.Events = new JwtBearerEvents
        //            {
        //                OnChallenge = async context =>
        //                {
        //                    context.HandleResponse();

        //                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
        //                    context.Response.ContentType = "application/json";

        //                    var response = ApiResponse.Fail(["Invalid token or token expired."], "Unauthorized");
        //                    await context.Response.WriteAsJsonAsync(response);
        //                },

        //                OnForbidden = async context =>
        //                {
        //                    context.Response.StatusCode = StatusCodes.Status403Forbidden;
        //                    context.Response.ContentType = "application/json";

        //                    var response = ApiResponse.Fail(["You don’t have access."], "Forbidden");
        //                    await context.Response.WriteAsJsonAsync(response);
        //                },

        //                OnAuthenticationFailed = async context =>
        //                {
        //                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
        //                    context.Response.ContentType = "application/json";

        //                    var response = ApiResponse.Fail(["Authentication failed."], "Unauthorized");
        //                    await context.Response.WriteAsJsonAsync(response);
        //                }
        //            };
        //        });

        //    return services;
        //}

        //public static IServiceCollection AddBasicAuthentication(this IServiceCollection services, IConfiguration configuration)
        //{
        //    services.Configure<BasicAuthSettings>(configuration.GetSection("Authentication:Schemes:Basic"));

        //    services
        //        .AddAuthentication(BasicAuthOptions.DefaultScheme)
        //        .AddScheme<BasicAuthOptions, BasicAuthenticationHandler>(
        //            BasicAuthOptions.DefaultScheme,
        //            _ => { }
        //        );

        //    return services;
        //}

        //public static IServiceCollection AddApiKeyAuthentication(this IServiceCollection services, IConfiguration configuration)
        //{
        //    services.Configure<ApiKeyAuthSettings>(configuration.GetSection("Authentication:Schemes:ApiKey"));

        //    services
        //        .AddAuthentication(ApiKeyAuthOptions.DefaultScheme)
        //        .AddScheme<ApiKeyAuthOptions, ApiKeyAuthenticationHandler>(
        //            ApiKeyAuthOptions.DefaultScheme,
        //            options => { }
        //        );

        //    return services;
        //}
    }
}
