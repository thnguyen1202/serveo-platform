using Serveo.Application.Dtos.Ordering;
using Serveo.Application.Dtos.Tenanting;
using Serveo.Application.Features.Catalog.Products.Create;
using Serveo.Application.Features.Identity.Auth.Login;
using Serveo.Application.Features.Ordering.Tables.Create;
using Serveo.Application.Features.Tenanting.Branches.Create;
using Serveo.Application.Features.Tenanting.RegisterTenant;
using Serveo.WebApi.Models.Auth;
using Serveo.WebApi.Models.Catalog.Products;
using Serveo.WebApi.Models.Ordering.Tables;
using Serveo.WebApi.Models.Public;
using Serveo.WebApi.Models.Tenanting.Outlets;

namespace Serveo.WebApi
{
    public static class MappingProfile
    {
        public static IServiceCollection AddWebApiMappingProfile(this IServiceCollection services)
        {
            _ = services.AddAutoMapper(cfg =>
            {
                cfg.CreateMap<LoginResult, LoginResponse>();

                cfg.CreateMap<CreateBranchResult, CreateOutletResponse>();

                cfg.CreateMap<CreateProductRequest, CreateProductCommand>();

                cfg.CreateMap<CreateTableRequest, CreateTableCommand>();


                // ===================
                // Public API Mapping
                // ===================
                cfg.CreateMap<BusinessDto, InitBusinessResponse>();
                cfg.CreateMap<BranchDto, InitBranchResponse>();
                cfg.CreateMap<TableDto, InitTableResponse>();
                cfg.CreateMap<RegisterTenantRequest, RegisterTenantCommand>()
                    .ForMember(d => d.OwnerName, opt => opt.MapFrom(s => s.FullName));
            });

            return services;
        }
    }
}
