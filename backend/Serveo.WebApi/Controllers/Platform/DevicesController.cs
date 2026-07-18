using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serveo.Application.Abstractions.Mediator;

namespace Serveo.WebApi.Controllers.Platform
{
    [Authorize]
    [Route("api/devices")]
    [ApiController]
    [Tags(ApiTags.Platform)]
    public class DevicesController(IMediator mediator, IMapper mapper) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<IActionResult> Register(CancellationToken ct)
        {
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(CancellationToken ct)
        {
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> Get(CancellationToken ct)
        {
            return Ok();
        }

        [HttpPatch]
        public async Task<IActionResult> Update(Guid id, CancellationToken ct)
        {
            return Ok();
        }
    }
}
