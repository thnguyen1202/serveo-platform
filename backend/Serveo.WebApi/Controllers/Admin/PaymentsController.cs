using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serveo.Application.Abstractions.Mediator;

namespace Serveo.WebApi.Controllers.Admin
{
    [Authorize]
    [Route("api/payments")]
    [ApiController]
    [Tags(ApiTags.Admin)]
    public class PaymentsController(IMediator mediator, IMapper mapper) : ControllerBase
    {
        [HttpPost("create")]
        public async Task<IActionResult> CreatePayment(CancellationToken ct)
        {
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> Get(Guid id, CancellationToken ct)
        {
            return Ok();
        }


        [HttpPost("webhook")]
        public async Task<IActionResult> Webhook(Guid id, CancellationToken ct)
        {
            return Ok();
        }

        [HttpGet("history")]
        public async Task<IActionResult> History(Guid id, CancellationToken ct)
        {
            return Ok();
        }
    }
}
