using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serveo.Application.Abstractions.Mediator;
using Serveo.Application.Dtos.Tenanting;
using Serveo.Application.Features;
using Serveo.Application.Features.Tenanting.Branches.Create;
using Serveo.Application.Features.Tenanting.Branches.Get;
using Serveo.Application.Services;
using Serveo.WebApi.Common;
using Serveo.WebApi.Models.Tenanting.Outlets;

namespace Serveo.WebApi.Controllers
{
    //[Authorize(AuthenticationSchemes = $"{ApiKeyAuthOptions.DefaultScheme}")]
    [Authorize]
    [Route("api/outlets")]
    [ApiController]
    public class OutletsController(IMediator mediator, IMapper mapper) : ControllerBase
    {

        protected readonly IMediator _mediator = mediator;
        private readonly IMapper _mapper = mapper;

        [HttpGet]
        [ProducesResponseType<PagedResult<BranchDto>>(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(CancellationToken ct)
        {
            var result = await _mediator.SendAsync(new PageBranchCommand(new PageQuery()), ct);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType<CreateOutletResponse>(StatusCodes.Status200OK)]
        public async Task<IActionResult> Post([FromBody] CreateOutletRequest req, CancellationToken ct)
        {
            var result = await _mediator.SendAsync(new CreateBranchCommand(req.Name, req.Address), ct);

            return this.ToActionResult(result, x => Ok(_mapper.Map<CreateOutletResponse>(x)));
        }
    }
}
