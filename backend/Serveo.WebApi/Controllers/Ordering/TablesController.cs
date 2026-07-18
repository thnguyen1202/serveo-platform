using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serveo.Application.Abstractions.Mediator;
using Serveo.Application.Features;
using Serveo.Application.Features.Ordering.Tables.Create;
using Serveo.Application.Features.Ordering.Tables.Get;
using Serveo.WebApi.Common;
using Serveo.WebApi.Models.Ordering.Tables;

namespace Serveo.WebApi.Controllers.Ordering
{
    //[Authorize(AuthenticationSchemes = $"{ApiKeyAuthOptions.DefaultScheme}")]
    [Authorize]
    [Route("api/tables")]
    [ApiController]
    public class TablesController(IMediator mediator, IMapper mapper) : ControllerBase
    {

        protected readonly IMediator _mediator = mediator;
        private readonly IMapper _mapper = mapper;

        [HttpGet]
        //[ProducesResponseType<PagedResult<CategoryDto>>(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(CancellationToken ct)
        {
            var result = await _mediator.SendAsync(new PageTableCommand(new PageQuery()), ct);
            return Ok(result);
        }

        [HttpPost]
        //[ProducesResponseType<CreateOutletResponse>(StatusCodes.Status200OK)]
        public async Task<IActionResult> Post([FromBody] CreateTableRequest req, CancellationToken ct)
        {
            var command = _mapper.Map<CreateTableCommand>(req);
            var result = await _mediator.SendAsync(command, ct);

            return this.ToActionResult(result);
        }
    }
}
