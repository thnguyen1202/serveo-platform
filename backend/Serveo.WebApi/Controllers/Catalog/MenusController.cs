using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serveo.Application.Abstractions.Mediator;
using Serveo.Application.Dtos.Catalog;
using Serveo.Application.Features;
using Serveo.Application.Features.Catalog.Menus.Create;
using Serveo.Application.Features.Catalog.Menus.Get;
using Serveo.Application.Features.Catalog.Menus.Items.Create;
using Serveo.Application.Services;
using Serveo.WebApi.Common;
using Serveo.WebApi.Extensions;
using Serveo.WebApi.Models;
using Serveo.WebApi.Models.Catalog.Memus;
using Serveo.WebApi.Models.Tenanting.Outlets;

namespace Serveo.WebApi.Controllers.Admin.Catalog
{
    //[Authorize(AuthenticationSchemes = $"{ApiKeyAuthOptions.DefaultScheme}")]
    [Authorize]
    [Route("api/menus")]
    [ApiController]
    [Tags(ApiTags.Catalog)]
    public class MenusController(IMediator mediator, PayloadMapper mapper) : ControllerBase
    {

        [HttpGet]
        [ProducesResponseType<PagedResult<MenuDto>>(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(int page, int size, CancellationToken ct)
        {
            if (page < 1) page = 1;
            if (size < 1) size = 10;

            var result = await mediator.SendAsync(new PageMenuCommand(new PageQuery(page, size)), ct);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType<CreateOutletResponse>(StatusCodes.Status200OK)]
        public async Task<IActionResult> Post([FromBody] CreateMenuRequest req, CancellationToken ct)
        {
            if (!User.TryGetBusinessId(out var businessId))
                businessId = req.BusinessId;

            var result = await mediator.SendAsync(new CreateMenuCommand(businessId ?? Guid.Empty, req.Name, req.Description), ct);

            return this.ToActionResult(result, x => Ok(mapper.ToResponse(x)));
        }

        [HttpPost("{id}/items")]
        [ProducesResponseType<CreateOutletResponse>(StatusCodes.Status200OK)]
        public async Task<IActionResult> Items(Guid id, [FromBody] CreateMenuItemRequest req, CancellationToken ct)
        {
            var result = await mediator.SendAsync(new CreateMenuItemCommand(id, req.ProductIds), ct);

            return this.ToActionResult(result);
        }

        #region Menu
        #endregion
    }
}
