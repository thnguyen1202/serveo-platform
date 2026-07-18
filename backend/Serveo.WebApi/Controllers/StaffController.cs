using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serveo.Application.Abstractions.Mediator;

namespace Serveo.WebApi.Controllers
{
    [Authorize]
    [Route("api/staff")]
    [ApiController]
    [Tags("10. Staff")]
    public class StaffController(IMediator mediator, IMapper mapper) : ControllerBase
    {
        [HttpGet("orders")]
        public async Task<IActionResult> Orders(CancellationToken ct)
        {
            return Ok();
        }

        [HttpGet("tables")]
        public async Task<IActionResult> Tables(CancellationToken ct)
        {
            return Ok();
        }


        [HttpPatch("tables/{id}/clean")]
        public async Task<IActionResult> MarkTableAsClean(Guid id, CancellationToken ct)
        {
            return Ok();
        }

        [HttpPatch("orders/{id}/served")]
        public async Task<IActionResult> MarkOrderAsServed(Guid id, CancellationToken ct)
        {
            return Ok();
        }

        [HttpPost("call-response")]
        public async Task<IActionResult> MarkCallResponse(Guid id, CancellationToken ct)
        {
            return Ok();
        }

        [HttpPost("request-bill-response")]
        public async Task<IActionResult> MarkRequestBillResponse(Guid id, CancellationToken ct)
        {
            return Ok();
        }
    }
}
