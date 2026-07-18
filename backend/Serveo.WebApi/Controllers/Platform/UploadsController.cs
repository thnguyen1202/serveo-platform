using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serveo.Application.Abstractions.Mediator;

namespace Serveo.WebApi.Controllers.Platform
{
    [Authorize]
    [Route("api/uploads")]
    [ApiController]
    [Tags(ApiTags.Platform)]
    public class UploadsController(IMediator mediator, IMapper mapper) : ControllerBase
    {
        [HttpPost("image")]
        public async Task<IActionResult> UploadImage(IFormFile file, CancellationToken ct)
        {
            return Ok();
        }

        [HttpPost("menu-image")]
        public async Task<IActionResult> UploadMenuImage(IFormFile file, CancellationToken ct)
        {
            return Ok();
        }


        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
        {
            return Ok();
        }
    }
}
