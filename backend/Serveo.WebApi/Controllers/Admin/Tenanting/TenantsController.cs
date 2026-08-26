using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serveo.Application.Abstractions.Mediator;
using Serveo.Application.Features;
using Serveo.Application.Features.Tenanting.Tenants.Paging;
using Serveo.Domain.Interfaces;

namespace Serveo.WebApi.Controllers.Admin.Tenanting
{
    [Authorize]
    [Route("api/tenants")]
    [ApiController]
    [Tags(ApiTags.Tenanting)]
    public class TenantsController(IMediator mediator, IMapper mapper, ISessionContext session) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get(CancellationToken ct)
        {
            var result = await mediator.SendAsync(new PageTenantCommand(new PageQuery()), ct);
            return Ok(result);
        }
    }
}
