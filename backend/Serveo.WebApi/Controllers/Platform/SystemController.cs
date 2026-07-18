using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serveo.Application.Abstractions.Mediator;

namespace Serveo.WebApi.Controllers.Platform
{
    [Authorize]
    [Route("api/system")]
    [ApiController]
    [Tags(ApiTags.Platform)]
    public class SystemController(IMediator mediator, IMapper mapper) : ControllerBase
    {
        [HttpGet("health")]
        public async Task<IActionResult> Health(CancellationToken ct)
        {
            return Ok();
        }


        [HttpGet("version")]
        public async Task<IActionResult> Version(CancellationToken ct)
        {
            return Ok();
        }

        [HttpGet("time")]
        public async Task<IActionResult> Time(CancellationToken ct)
        {
            return Ok();
        }

        [HttpGet("features")]
        public async Task<IActionResult> Features(CancellationToken ct)
        {
            return Ok();
        }
    }
}
