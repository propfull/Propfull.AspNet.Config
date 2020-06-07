using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Propfull.AspNet.Config.Sample.Controllers
{
    [ApiController]
    [Route("")]
    public class AboutController : ControllerBase
    {
        private readonly ConfigService<ApiConfig> configService;

        public AboutController(ConfigService<ApiConfig> configService)
        {
            this.configService = configService;
        }

        public async Task<IActionResult> GetAboutAsync()
        {
            var config = await configService.GetConfigAsync();
            return Ok(config);
        }
    }
}
