using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serveo.Application.Abstractions.Mediator;

namespace Serveo.WebApi.Controllers.Platform
{
    [Authorize]
    [Route("api/qr")]
    [ApiController]
    [Tags(ApiTags.Platform)]
    public class QrController(IMediator mediator, IMapper mapper) : ControllerBase
    {
        [HttpGet("tables")]
        public async Task<IActionResult> Tables(CancellationToken ct)
        {
            return Ok();
        }

        [HttpPost("generate")]
        public async Task<IActionResult> Generate(CancellationToken ct)
        {
            return Ok();
        }

        [HttpPost("regenerate")]
        public async Task<IActionResult> Regenerate(CancellationToken ct)
        {
            return Ok();
        }

        [HttpGet("download")]
        public async Task<IActionResult> Download(CancellationToken ct)
        {
            return Ok();
        }
    }
}
