using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serveo.Application.Abstractions.Mediator;

namespace Serveo.WebApi.Controllers.Admin
{
    [Authorize]
    [Route("api/integrations")]
    [ApiController]
    [Tags(ApiTags.Admin)]
    public class IntegrationsController(IMediator mediator, IMapper mapper) : ControllerBase
    {
        [HttpPost("grabfood")]
        public async Task<IActionResult> Grabfood(CancellationToken ct)
        {
            return Ok();
        }

        [HttpPost("shopeefood")]
        public async Task<IActionResult> Shopeefood(CancellationToken ct)
        {
            return Ok();
        }


        [HttpPost("be")]
        public async Task<IActionResult> Be(Guid id, CancellationToken ct)
        {
            return Ok();
        }

        [HttpPost("momo")]
        public async Task<IActionResult> Momo(Guid id, CancellationToken ct)
        {
            return Ok();
        }

        [HttpPost("vnpay")]
        public async Task<IActionResult> Vnpay(Guid id, CancellationToken ct)
        {
            return Ok();
        }
    }
}
