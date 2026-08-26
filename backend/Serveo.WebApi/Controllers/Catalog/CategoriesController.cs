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
using Serveo.WebApi.Models.Catalog.Categories;
using Serveo.WebApi.Models.Tenanting.Outlets;

namespace Serveo.WebApi.Controllers.Admin.Catalog
{
    //[Authorize(AuthenticationSchemes = $"{ApiKeyAuthOptions.DefaultScheme}")]
    [Authorize]
    [Route("api/categories")]
    [ApiController]
    [Tags(ApiTags.Catalog)]
    public class CategoriesController(IMediator mediator, IMapper mapper) : ControllerBase
    {

        protected readonly IMediator _mediator = mediator;
        private readonly IMapper _mapper = mapper;

        [HttpGet]
        [ProducesResponseType<PagedResult<CategoryDto>>(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(CancellationToken ct)
        {
            var result = await _mediator.SendAsync(new PageCategoryCommand(new PageQuery()), ct);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType<CreateOutletResponse>(StatusCodes.Status200OK)]
        public async Task<IActionResult> Post([FromBody] CreateCategoryRequest req, CancellationToken ct)
        {
            var result = await _mediator.SendAsync(new CreateCategoryCommand(req.BusinessId, req.Name), ct);

            return this.ToActionResult(result, x => Ok(_mapper.Map<CreateOutletResponse>(x)));
        }
    }
}
