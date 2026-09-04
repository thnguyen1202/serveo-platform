using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serveo.Application.Abstractions.Mediator;
using Serveo.Application.Dtos.Catalog;
using Serveo.Application.Features;
using Serveo.Application.Features.Catalog.Categories.Create;
using Serveo.Application.Features.Catalog.Categories.Get;
using Serveo.Application.Services;
using Serveo.WebApi.Common;
using Serveo.WebApi.Extensions;
using Serveo.WebApi.Models;
using Serveo.WebApi.Models.Catalog.Categories;
using Serveo.WebApi.Models.Tenanting.Outlets;

namespace Serveo.WebApi.Controllers.Admin.Catalog
{
    //[Authorize(AuthenticationSchemes = $"{ApiKeyAuthOptions.DefaultScheme}")]
    [Authorize]
    [Route("api/categories")]
    [ApiController]
    [Tags(ApiTags.Catalog)]
    public class CategoriesController(IMediator mediator, PayloadMapper mapper) : ControllerBase
    {

        [HttpGet]
        [ProducesResponseType<PagedResult<CategoryDto>>(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(CancellationToken ct)
        {
            var result = await mediator.SendAsync(new PageCategoryCommand(new PageQuery()), ct);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType<CreateCategoryResponse>(StatusCodes.Status200OK)]
        public async Task<IActionResult> Post([FromBody] CreateCategoryRequest req, CancellationToken ct)
        {
            if (!User.TryGetBusinessId(out var businessId))
                businessId = req.BusinessId;

            var result = await mediator.SendAsync(new CreateCategoryCommand(businessId ?? Guid.Empty, req.Name), ct);

            return this.ToActionResult(result, x => Ok(mapper.ToResponse(x)));
        }
    }
}
