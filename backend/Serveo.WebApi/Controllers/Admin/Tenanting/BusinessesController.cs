using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serveo.Application.Abstractions.Mediator;
using Serveo.Application.Features;
using Serveo.Application.Features.Tenanting.Businesses.Paging;

namespace Serveo.WebApi.Controllers.Admin.Tenanting
{
    /*
     Business quản lý:

Branch
Menu
Product template
Category
Promotion
     */
    [Authorize]
    [Route("api/businesses")]
    [ApiController]
    [Tags(ApiTags.Tenanting)]
    public class BusinessesController(IMediator mediator, IMapper mapper) : ControllerBase
    {
        [HttpGet("dashboard")]
        public async Task<IActionResult> Dashboard(CancellationToken ct)
        {
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> Get(CancellationToken ct)
        {
            var result = await mediator.SendAsync(new PageBusinessCommand(new PageQuery()), ct);
            return Ok(result);
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

        [HttpGet("branches")]
        public async Task<IActionResult> GetBranches(CancellationToken ct)
        {
            return Ok();
        }

        [HttpPost("branches")]
        public async Task<IActionResult> CreateBranch(CancellationToken ct)
        {
            return Ok();
        }

        [HttpPut("branches/{id}")]
        public async Task<IActionResult> UpdateBranch(Guid id, CancellationToken ct)
        {
            return Ok();
        }

        [HttpDelete("branches/{id}")]
        public async Task<IActionResult> DeleteBranch(Guid id, CancellationToken ct)
        {
            return Ok();
        }

        [HttpGet("products")]
        public async Task<IActionResult> GetProducts(CancellationToken ct)
        {
            return Ok();
        }

        [HttpPost("products")]
        public async Task<IActionResult> CreateProduct(CancellationToken ct)
        {
            return Ok();
        }

        [HttpPut("products/{id}")]
        public async Task<IActionResult> UpdateProduct(Guid id, CancellationToken ct)
        {
            return Ok();
        }

        [HttpDelete("products/{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id, CancellationToken ct)
        {
            return Ok();
        }
    }
}
