using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serveo.Application.Abstractions.Mediator;

namespace Serveo.WebApi.Controllers.Admin.Tenanting
{
    /*
     Branch quản lý:

Order
Table
Kitchen
Staff assignment
Printer
     */
    [Authorize]
    [Route("api/branches")]
    [ApiController]
    [Tags(ApiTags.Tenanting)]
    public class BranchesController(IMediator mediator, IMapper mapper) : ControllerBase
    {
        [HttpGet("dashboard")]
        public async Task<IActionResult> Dashboard(CancellationToken ct)
        {
            return Ok();
        }


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

        [HttpGet("tables")]
        public async Task<IActionResult> Tables(CancellationToken ct)
        {
            return Ok();
        }

        [HttpPut("tables")]
        public async Task<IActionResult> UpdateTables(CancellationToken ct)
        {
            return Ok();
        }

        [HttpGet("orders")]
        public async Task<IActionResult> Orders(CancellationToken ct)
        {
            return Ok();
        }

        [HttpGet("staff")]
        public async Task<IActionResult> Staff(CancellationToken ct)
        {
            return Ok();
        }

        [HttpGet("current")]
        public async Task<IActionResult> Current(CancellationToken ct)
        {
            return Ok();
        }

        [HttpPut("current")]
        public async Task<IActionResult> UpdateCurrent(CancellationToken ct)
        {
            return Ok();
        }


        [HttpGet("current/open-hours")]
        public async Task<IActionResult> GetOpenHours(Guid id, CancellationToken ct)
        {
            return Ok();
        }

        [HttpPut("current/open-hours")]
        public async Task<IActionResult> UpdateOpenHours(Guid id, CancellationToken ct)
        {
            return Ok();
        }

        [HttpPatch("current/status")]
        public async Task<IActionResult> UpdateStatus(Guid id, CancellationToken ct)
        {
            return Ok();
        }

        [HttpGet("roles")]
        public async Task<IActionResult> GetRoles(Guid id, CancellationToken ct)
        {
            return Ok();
        }
    }
}
