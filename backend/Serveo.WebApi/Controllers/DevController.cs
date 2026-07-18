using Microsoft.AspNetCore.Mvc;
using Serveo.Domain.Helpers;
using Serveo.Infrastructure.Authentication.Passwords;
using Serveo.SharedKernel;

namespace Serveo.WebApi.Controllers
{
    [ApiController]
    [Route("api/dev")]
    [Tags(ApiTags.Development)]
    public class DevController : ControllerBase
    {
        [HttpGet("TestEncodeId")]
        public IActionResult TestEncodeId(long input)
        {
            return Ok(new { data = input });
        }

        [HttpGet("TokenGenerator")]
        public IActionResult TokenGenerator(int size = 128)
        {
            var token = PublicTokenGenerator.Generate(size);
            return Ok(new { token = token, length = token.Length });
        }

        [HttpGet("PasswordHasher")]
        public IActionResult PasswordHasher(string password)
        {
            var passwordHasherService = new PasswordHasherService();
            var hashedPassword = passwordHasherService.Hash(password);

            return Ok(new { key = hashedPassword, length = hashedPassword.Length });
        }


        [HttpGet("ApiKeyGenerator")]
        public IActionResult ApiKeyGenerator()
        {
            var first = Base62.EncodeGuid(Guid.CreateVersion7());
            var last = Base62.Encode(Base62.LongToSpan(DateTime.UtcNow.Ticks));
            var key = $"{first}-{last}";
            return Ok(new { key = key, length = key.Length });
        }

        private static readonly string[] Summaries =
        [
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        ];

        //[HttpGet(Name = "GetWeather")]
        //[ProducesResponseType<List<WeatherForecast>>(StatusCodes.Status200OK)]
        //public IActionResult Get()
        //{
        //    var value = Enumerable.Range(1, 5).Select(index => new WeatherForecast
        //    {
        //        Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
        //        TemperatureC = Random.Shared.Next(-20, 55),
        //        Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        //    })
        //    .ToList();
        //    var result = CommandResult<List<WeatherForecast>>.Success(value);

        //    return this.ToActionResult(result);
        //}
    }
}
