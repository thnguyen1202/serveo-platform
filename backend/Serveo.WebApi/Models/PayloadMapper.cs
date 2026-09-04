using Riok.Mapperly.Abstractions;
using Serveo.Application.Features.Catalog.Categories.Create;
using Serveo.Application.Features.Catalog.Menus.Create;
using Serveo.Application.Features.Catalog.Products.Create;
using Serveo.Application.Features.Identity.Auth.Login;
using Serveo.Application.Features.Identity.Auth.RefreshToken;
using Serveo.Application.Features.Ordering.Tables.Create;
using Serveo.WebApi.Models.Auth;
using Serveo.WebApi.Models.Catalog.Categories;
using Serveo.WebApi.Models.Catalog.Memus;
using Serveo.WebApi.Models.Catalog.Products;
using Serveo.WebApi.Models.Ordering.Tables;

namespace Serveo.WebApi.Models
{
    [Mapper(RequiredMappingStrategy = RequiredMappingStrategy.None)]
    public partial class PayloadMapper
    {
        public partial LoginResponse ToResponse(LoginResult result);
        public partial RefreshTokenResponse ToResponse(RefreshTokenResult result);
        public partial CreateMenuResponse ToResponse(CreateMenuResult result);
        public partial CreateCategoryResponse ToResponse(CreateCategoryResult result);
        public partial CreateTableResponse ToResponse(CreateTableResult result);

        public partial CreateProductCommand ToCommand(CreateProductRequest result);

    }
}
