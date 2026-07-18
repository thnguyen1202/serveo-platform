using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serveo.Application.Abstractions.Mediator;

namespace Serveo.WebApi.Controllers.Platform
{
    [Authorize]
    [Route("api/notifications")]
    [ApiController]
    [Tags(ApiTags.Platform)]
    public class NotificationsController(IMediator mediator, IMapper mapper) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get(CancellationToken ct)
        {
            return Ok();
        }

        [HttpPatch("read")]
        public async Task<IActionResult> Read(CancellationToken ct)
        {
            return Ok();
        }


        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
        {
            return Ok();
        }

        [HttpGet("unread-count")]
        public async Task<IActionResult> GetUnreadCount(CancellationToken ct)
        {
            return Ok();
        }
    }
}
