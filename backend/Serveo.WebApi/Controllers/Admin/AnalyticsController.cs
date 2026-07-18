using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serveo.Application.Abstractions.Mediator;

namespace Serveo.WebApi.Controllers.Admin
{
    [Authorize]
    [Route("api/analytics")]
    [ApiController]
    [Tags(ApiTags.Admin)]
    public class AnalyticsController(IMediator mediator, IMapper mapper) : ControllerBase
    {
        [HttpGet("dashboard")]
        public async Task<IActionResult> Dashboard(IFormFile file, CancellationToken ct)
        {
            return Ok();
        }

        [HttpGet("revenue")]
        public async Task<IActionResult> Revenue(IFormFile file, CancellationToken ct)
        {
            return Ok();
        }


        [HttpGet("popular-products")]
        public async Task<IActionResult> PopularProducts(IFormFile file, CancellationToken ct)
        {
            return Ok();
        }

        [HttpGet("customers")]
        public async Task<IActionResult> Customers(IFormFile file, CancellationToken ct)
        {
            return Ok();
        }

        [HttpGet("hourly-sales")]
        public async Task<IActionResult> HourlySales(IFormFile file, CancellationToken ct)
        {
            return Ok();
        }

        [HttpGet("orders")]
        public async Task<IActionResult> Orders(IFormFile file, CancellationToken ct)
        {
            return Ok();
        }
    }
}
