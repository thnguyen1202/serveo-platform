using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serveo.Application.Abstractions.Mediator;
using Serveo.Application.Dtos.Ordering;
using Serveo.Application.Features;
using Serveo.Application.Features.Ordering.Tables.Create;
using Serveo.Application.Features.Ordering.Tables.Get;
using Serveo.Application.Services;
using Serveo.WebApi.Common;
using Serveo.WebApi.Extensions;
using Serveo.WebApi.Models;
using Serveo.WebApi.Models.Ordering.Tables;

namespace Serveo.WebApi.Controllers.Admin
{
    [Authorize]
    [Route("api/tables")]
    [ApiController]
    [Tags(ApiTags.Admin)]
    public class TablesController(IMediator mediator, PayloadMapper mapper) : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType<PagedResult<TableDto>>(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(CancellationToken ct)
        {
            var result = await mediator.SendAsync(new PageTableCommand(new PageQuery()), ct);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType<CreateTableResponse>(StatusCodes.Status200OK)]
        public async Task<IActionResult> Post([FromBody] CreateTableRequest req, CancellationToken ct)
        {
            if (!User.TryGetBranchId(out var branchId))
                branchId = req.BranchId;

            var result = await mediator.SendAsync(new CreateTableCommand(branchId ?? Guid.Empty, req.Name, req.Capacity), ct);

            return this.ToActionResult(result, x => Ok(mapper.ToResponse(x)));
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreatePayment(CancellationToken ct)
        {
            return Ok();
        }


        [HttpPost("webhook")]
        public async Task<IActionResult> Webhook(Guid id, CancellationToken ct)
        {
            return Ok();
        }

        [HttpGet("history")]
        public async Task<IActionResult> History(Guid id, CancellationToken ct)
        {
            return Ok();
        }
    }
}
