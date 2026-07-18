using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serveo.Application.Abstractions.Mediator;
using Serveo.Application.Features.Tenanting.Tenant.Profile;
using Serveo.Domain.Interfaces;
using Serveo.WebApi.Common;

namespace Serveo.WebApi.Controllers.Admin.Tenanting
{
    [Authorize]
    [Route("api/tenant")]
    [ApiController]
    [Tags(ApiTags.Tenanting)]
    public class TenantController(IMediator mediator, IMapper mapper, ISessionContext session) : ControllerBase
    {
        [HttpGet("dashboard")]
        public async Task<IActionResult> Dashboard(CancellationToken ct)
        {

            return Ok();
        }

        [HttpGet("profile")]
        public async Task<IActionResult> Profile(CancellationToken ct)
        {
            var result = await mediator.SendAsync(new GetTenantProfileCommand(session.TenantId ?? Guid.Empty), ct);
            return this.ToActionResult(result);
        }

        [HttpPut("profile")]
        public async Task<IActionResult> UpdateProfile(CancellationToken ct)
        {
            return Ok();
        }

        [HttpGet("businesses")]
        public async Task<IActionResult> GetBusinesses(CancellationToken ct)
        {
            return Ok();
        }

        [HttpPost("businesses")]
        public async Task<IActionResult> CreateBusiness(CancellationToken ct)
        {
            return Ok();
        }

        [HttpPut("businesses/{id}")]
        public async Task<IActionResult> UpdateBusiness(Guid id, CancellationToken ct)
        {
            return Ok();
        }

        [HttpDelete("businesses/{id}")]
        public async Task<IActionResult> DeleteBusiness(Guid id, CancellationToken ct)
        {
            return Ok();
        }


        [HttpGet("subscription")]
        public async Task<IActionResult> GetSubscription(Guid id, CancellationToken ct)
        {
            return Ok();
        }

        [HttpGet("billing")]
        public async Task<IActionResult> GetBilling(Guid id, CancellationToken ct)
        {
            return Ok();
        }

        [HttpGet("usage")]
        public async Task<IActionResult> GetUsage(Guid id, CancellationToken ct)
        {
            return Ok();
        }

        [HttpGet("invoices")]
        public async Task<IActionResult> GetInvoices(Guid id, CancellationToken ct)
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
