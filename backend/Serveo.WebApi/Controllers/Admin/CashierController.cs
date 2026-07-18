using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serveo.Application.Abstractions.Mediator;

namespace Serveo.WebApi.Controllers.Admin
{
    [Authorize]
    [Route("api/cashier")]
    [ApiController]
    [Tags(ApiTags.Admin)]
    public class CashierController(IMediator mediator, IMapper mapper) : ControllerBase
    {
        [HttpGet("orders")]
        public async Task<IActionResult> Orders(CancellationToken ct)
        {
            return Ok();
        }

        [HttpPost("payment")]
        public async Task<IActionResult> ProcessPayment(CancellationToken ct)
        {
            return Ok();
        }

        [HttpPost("refund")]
        public async Task<IActionResult> ProcessRefund(CancellationToken ct)
        {
            return Ok();
        }

        [HttpPatch("orders/{id}/complete")]
        public async Task<IActionResult> MarkOrderAsComplete(Guid id, CancellationToken ct)
        {
            return Ok();
        }

        [HttpPatch("orders/{id}/cancel")]
        public async Task<IActionResult> MarkOrderAsCanceled(Guid id, CancellationToken ct)
        {
            return Ok();
        }
    }
}
