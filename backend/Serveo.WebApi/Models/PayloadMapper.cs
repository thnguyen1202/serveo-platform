using Riok.Mapperly.Abstractions;
using Serveo.Application.Features.Catalog.Menus.Create;
using Serveo.Application.Features.Identity.Auth.Login;
using Serveo.Application.Features.Identity.Auth.RefreshToken;
using Serveo.WebApi.Models.Auth;
using Serveo.WebApi.Models.Catalog.Memus;

namespace Serveo.WebApi.Models
{
    [Mapper(RequiredMappingStrategy = RequiredMappingStrategy.None)]
    public partial class PayloadMapper
    {
        public partial LoginResponse ToResponse(LoginResult result);
        public partial RefreshTokenResponse ToResponse(RefreshTokenResult result);
        public partial CreateMenuResponse ToResponse(CreateMenuResult result);
    }
}
