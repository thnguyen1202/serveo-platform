using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serveo.Application.Abstractions.Mediator;

namespace Serveo.WebApi.Controllers.Platform
{
    [Authorize]
    [Route("api/internal")]
    [ApiController]
    [Tags(ApiTags.Platform)]
    public class InternalController(IMediator mediator, IMapper mapper) : ControllerBase
    {
        [HttpGet("cache")]
        public async Task<IActionResult> Cache(CancellationToken ct)
        {
            return Ok();
        }


        [HttpGet("jobs")]
        public async Task<IActionResult> Jobs(CancellationToken ct)
        {
            return Ok();
        }

        [HttpGet("webhooks")]
        public async Task<IActionResult> Webhooks(CancellationToken ct)
        {
            return Ok();
        }

        [HttpGet("events")]
        public async Task<IActionResult> Events(CancellationToken ct)
        {
            return Ok();
        }
    }
}
