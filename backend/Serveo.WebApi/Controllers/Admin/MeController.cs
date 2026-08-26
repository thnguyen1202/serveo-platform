using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serveo.Application.Abstractions.Mediator;
using Serveo.Application.Features.Catalog.Menus;
using Serveo.Application.Features.Catalog.Menus.Get;
using Serveo.WebApi.Common;
using Serveo.WebApi.Models.Auth;

namespace Serveo.WebApi.Controllers.Admin
{
    [Authorize]
    [Route("api/me")]
    [ApiController]
    [Tags(ApiTags.Me)]
    public class MeController(IMediator mediator) : ControllerBase
    {
        [HttpGet("profile")]
        public async Task<IActionResult> GetProfile(CancellationToken ct)
        {
            return Ok();
        }

        [HttpGet("menus")]
        [ProducesResponseType<IEnumerable<MenuOption>>(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetMenus(CancellationToken ct)
        {
            var result = await mediator.SendAsync(new MyMenuCommand(), ct);
            return Ok(result);
        }

        [HttpGet("menus/options")]
        public async Task<IActionResult> GetMenusOptions(CancellationToken ct)
        {
            return Ok();
        }

        [HttpGet("permissions")]
        public async Task<IActionResult> GetPermissions(CancellationToken ct)
        {
            return Ok();
        }

        [HttpGet("roles")]
        public async Task<IActionResult> GetRoles(CancellationToken ct)
        {
            return Ok();
        }



    }
}
