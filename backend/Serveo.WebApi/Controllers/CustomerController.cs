using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serveo.Application.Abstractions.Mediator;

namespace Serveo.WebApi.Controllers
{
    // Khách hàng đã đăng nhập (nếu có).
    [Authorize]
    [Route("api/customer")]
    [ApiController]
    [Tags(ApiTags.Customer)]
    public class CustomerController(IMediator mediator, IMapper mapper) : ControllerBase
    {
        [HttpGet("profile")]
        public async Task<IActionResult> Profile(CancellationToken ct)
        {
            return Ok();
        }

        [HttpPut("profile")]
        public async Task<IActionResult> UpdateProfile(CancellationToken ct)
        {
            return Ok();
        }

        [HttpGet("orders")]
        public async Task<IActionResult> Orders(CancellationToken ct)
        {
            return Ok();
        }

        [HttpGet("favorites")]
        public async Task<IActionResult> Gavorites(CancellationToken ct)
        {
            return Ok();
        }

        [HttpPost("favorites")]
        public async Task<IActionResult> AddToFavorites(CancellationToken ct)
        {
            return Ok();
        }
    }
}
