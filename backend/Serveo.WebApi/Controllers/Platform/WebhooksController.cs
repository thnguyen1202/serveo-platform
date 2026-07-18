using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serveo.Application.Abstractions.Mediator;

namespace Serveo.WebApi.Controllers.Platform
{
    [Authorize]
    [Route("api/webhooks")]
    [ApiController]
    [Tags(ApiTags.Platform)]
    public class WebhooksController(IMediator mediator, IMapper mapper) : ControllerBase
    {
        [HttpPost("payment")]
        public async Task<IActionResult> Payment(CancellationToken ct)
        {
            return Ok();
        }

        [HttpPost("pos")]
        public async Task<IActionResult> Pos(CancellationToken ct)
        {
            return Ok();
        }

        [HttpPost("delivery")]
        public async Task<IActionResult> Delivery(CancellationToken ct)
        {
            return Ok();
        }
    }
}
