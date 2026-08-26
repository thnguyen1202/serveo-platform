using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serveo.Application.Abstractions.Mediator;
using Serveo.Application.Features;
using Serveo.Application.Features.Catalog.Products.Create;
using Serveo.Application.Features.Catalog.Products.Get;
using Serveo.WebApi.Common;
using Serveo.WebApi.Models.Catalog.Products;

namespace Serveo.WebApi.Controllers.Admin.Catalog
{
    //[Authorize(AuthenticationSchemes = $"{ApiKeyAuthOptions.DefaultScheme}")]
    [Authorize]
    [Route("api/products")]
    [ApiController]
    [Tags(ApiTags.Catalog)]
    public class ProductsController(IMediator mediator, IMapper mapper) : ControllerBase
    {

        protected readonly IMediator _mediator = mediator;
        private readonly IMapper _mapper = mapper;

        [HttpGet]
        //[ProducesResponseType<PagedResult<CategoryDto>>(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(CancellationToken ct)
        {
            var result = await _mediator.SendAsync(new PageProductCommand(new PageQuery()), ct);
            return Ok(result);
        }

        [HttpPost]
        //[ProducesResponseType<CreateOutletResponse>(StatusCodes.Status200OK)]
        public async Task<IActionResult> Post([FromBody] CreateProductRequest req, CancellationToken ct)
        {
            var command = _mapper.Map<CreateProductCommand>(req);
            var result = await _mediator.SendAsync(command, ct);

            return this.ToActionResult(result);
        }
    }
}
