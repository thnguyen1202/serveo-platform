using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serveo.Application.Abstractions.Mediator;
using Serveo.Application.Features.Platform.CreateApiKey;

namespace Serveo.WebApi.Controllers.Platform
{
    [Authorize]
    [Route("api/support")]
    [ApiController]
    [Tags(ApiTags.Platform)]
    public class SupportController(IMediator mediator, IMapper mapper) : ControllerBase
    {
        [HttpPost("ticket")]
        public async Task<IActionResult> CreateTicket(CancellationToken ct)
        {
            return Ok();
        }

        [HttpGet("ticket")]
        public async Task<IActionResult> GetTicket(CancellationToken ct)
        {
            return Ok();
        }

        [HttpPost("chat")]
        public async Task<IActionResult> Chat(CancellationToken ct)
        {
            return Ok();
        }

        [AllowAnonymous]
        [HttpPost("api-key")]
        public async Task<IActionResult> CreateApiKey(CancellationToken ct)
        {
            var result = await mediator.SendAsync(new CreateApiKeyCommand("Development", "dev"), ct);
            return Ok(result);
        }
    }
}
