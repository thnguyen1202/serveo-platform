using Microsoft.Extensions.DependencyInjection;
using Serveo.Application.Dtos.Catalog;
using Serveo.Application.Dtos.Identity;
using Serveo.Application.Dtos.Ordering;
using Serveo.Application.Dtos.Tenanting;
using Serveo.Application.Features.Catalog.Categories.Create;
using Serveo.Application.Features.Catalog.Menus.Create;
using Serveo.Application.Features.Catalog.Products.Create;
using Serveo.Application.Features.Catalog.Products.Get;
using Serveo.Application.Features.Ordering.Tables.Create;
using Serveo.Application.Features.Tenanting.Branches.Create;
using Serveo.Domain.Entities.Catalog;
using Serveo.Domain.Entities.Identity;
using Serveo.Domain.Entities.Ordering;
using Serveo.Domain.Entities.Tenanting;

namespace Serveo.Application
{
    public static class MappingProfile
    {
        public static IServiceCollection AddApplicationMappingProfile(this IServiceCollection services)
        {
            _ = services.AddAutoMapper(cfg =>
            {
                cfg.CreateMap<User, UserDto>();
                cfg.CreateMap<Tenant, TenantDto>();
                cfg.CreateMap<Business, BusinessDto>();
                cfg.CreateMap<Branch, BranchDto>();

                cfg.CreateMap<Category, CategoryDto>();
                cfg.CreateMap<Product, ProductDto>();
                cfg.CreateMap<Menu, MenuDto>()
                    .ForMember(d => d.Products, opt => opt.MapFrom(s => s.Items.Select(mp => mp.Product)));

                cfg.CreateMap<Table, TableDto>();


                cfg.CreateMap<CreateBranchCommand, Branch>();
                cfg.CreateMap<Branch, CreateBranchResult>();

                cfg.CreateMap<Category, CreateCategoryResult>();

                cfg.CreateMap<Menu, CreateMenuResult>();

                cfg.CreateMap<Product, CreateProductResult>();

                cfg.CreateMap<Table, CreateTableResult>();

                cfg.CreateMap<Product, PublicProductResutl>();



            });

            return services;
        }
    }
}
