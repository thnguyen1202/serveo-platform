using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serveo.Application.Abstractions.Mediator;

namespace Serveo.WebApi.Controllers.Admin
{
    [Authorize]
    [Route("api/kitchen")]
    [ApiController]
    [Tags(ApiTags.Admin)]
    public class KitchenController(IMediator mediator, IMapper mapper) : ControllerBase
    {
        [HttpGet("orders")]
        public async Task<IActionResult> Orders(CancellationToken ct)
        {
            return Ok();
        }

        [HttpPatch("orders/{id}/accept")]
        public async Task<IActionResult> AcceptOrder(Guid id, CancellationToken ct)
        {
            return Ok();
        }

        [HttpPatch("orders/{id}/cooking")]
        public async Task<IActionResult> StartCooking(Guid id, CancellationToken ct)
        {
            return Ok();
        }

        [HttpPatch("orders/{id}/ready")]
        public async Task<IActionResult> MarkOrderAsReady(Guid id, CancellationToken ct)
        {
            return Ok();
        }

        [HttpPatch("order-items/{id}/ready")]
        public async Task<IActionResult> MarkOrderItemAsReady(Guid id, CancellationToken ct)
        {
            return Ok();
        }
    }
}
