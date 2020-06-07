using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Propfull.AspNet.Config.Sample.Controllers
{
    [ApiController]
    [Route("")]
    public class WeatherForecastController : ControllerBase
    {
        public async Task<IActionResult> GetAboutAsync()
        {
            return await Task.Run(() => Ok(new
            {
                Name = "Propfull sample API",
                Version = "v1.0.0"
            }));
        }
    }
}
