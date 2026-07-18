using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serveo.Application.Abstractions.Mediator;

namespace Serveo.WebApi.Controllers
{
    [Authorize]
    [Route("api/admin")]
    [ApiController]
    [Tags("03. Admin")]
    public class AdminController(IMediator mediator, IMapper mapper) : ControllerBase
    {
        [HttpGet("dashboard")]
        public async Task<IActionResult> Dashboard(CancellationToken ct)
        {
            return Ok();
        }

        [HttpGet("businesses")]
        public async Task<IActionResult> Businesses(CancellationToken ct)
        {
            return Ok();
        }

        [HttpGet("branches")]
        public async Task<IActionResult> Branches(CancellationToken ct)
        {
            return Ok();
        }

        [HttpGet("categories")]
        public async Task<IActionResult> Categories(CancellationToken ct)
        {
            return Ok();
        }

        [HttpGet("products")]
        public async Task<IActionResult> Products(CancellationToken ct)
        {
            return Ok();
        }

        [HttpPut("products/{id}")]
        public async Task<IActionResult> Products(Guid id, CancellationToken ct)
        {
            return Ok();
        }

        [HttpDelete("products/{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id, CancellationToken ct)
        {
            return Ok();
        }

        [HttpGet("menus")]
        public async Task<IActionResult> Menus(CancellationToken ct)
        {
            return Ok();
        }

        [HttpGet("tables")]
        public async Task<IActionResult> Tables(CancellationToken ct)
        {
            return Ok();
        }

        [HttpGet("table-groups")]
        public async Task<IActionResult> TableGroups(CancellationToken ct)
        {
            return Ok();
        }

        [HttpGet("orders")]
        public async Task<IActionResult> Orders(CancellationToken ct)
        {
            return Ok();
        }

        [HttpGet("orders/{id}")]
        public async Task<IActionResult> Orders(Guid id, CancellationToken ct)
        {
            return Ok();
        }


        [HttpPatch("orders/{id}/status")]
        public async Task<IActionResult> UpdateOrderStatus(Guid id, CancellationToken ct)
        {
            return Ok();
        }

        [HttpGet("customers")]
        public async Task<IActionResult> Customers(CancellationToken ct)
        {
            return Ok();
        }

        [HttpGet("promotions")]
        public async Task<IActionResult> Promotions(CancellationToken ct)
        {
            return Ok();
        }

        [HttpGet("staff")]
        public async Task<IActionResult> Staff(CancellationToken ct)
        {
            return Ok();
        }

        [HttpGet("roles")]
        public async Task<IActionResult> Roles(CancellationToken ct)
        {
            return Ok();
        }

        [HttpGet("permissions")]
        public async Task<IActionResult> Permissions(CancellationToken ct)
        {
            return Ok();
        }

        [HttpGet("settings")]
        public async Task<IActionResult> Settings(CancellationToken ct)
        {
            return Ok();
        }

        [HttpGet("reports")]
        public async Task<IActionResult> Reports(CancellationToken ct)
        {
            return Ok();
        }
    }
}
