using AutoMapper;
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
using Serveo.WebApi.Models.Catalog.Memus;
using Serveo.WebApi.Models.Tenanting.Outlets;

namespace Serveo.WebApi.Controllers.Admin.Catalog
{
    //[Authorize(AuthenticationSchemes = $"{ApiKeyAuthOptions.DefaultScheme}")]
    [Authorize]
    [Route("api/menus")]
    [ApiController]
    [Tags(ApiTags.Catalog)]
    public class MenusController(IMediator mediator, IMapper mapper) : ControllerBase
    {

        protected readonly IMediator _mediator = mediator;
        private readonly IMapper _mapper = mapper;

        [HttpGet]
        [ProducesResponseType<PagedResult<MenuDto>>(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(CancellationToken ct)
        {
            var result = await _mediator.SendAsync(new PageMenuCommand(new PageQuery()), ct);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType<CreateOutletResponse>(StatusCodes.Status200OK)]
        public async Task<IActionResult> Post([FromBody] CreateMenuRequest req, CancellationToken ct)
        {
            var result = await _mediator.SendAsync(new CreateMenuCommand(req.BusinessId, req.Name), ct);

            return this.ToActionResult(result, x => Ok(_mapper.Map<CreateOutletResponse>(x)));
        }

        [HttpPost("{id}/items")]
        [ProducesResponseType<CreateOutletResponse>(StatusCodes.Status200OK)]
        public async Task<IActionResult> Items(Guid id, [FromBody] CreateMenuItemRequest req, CancellationToken ct)
        {
            var result = await _mediator.SendAsync(new CreateMenuItemCommand(id, req.ProductIds), ct);

            return this.ToActionResult(result);
        }
    }
}
