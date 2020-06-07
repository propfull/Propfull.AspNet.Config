using Microsoft.Extensions.Options;

namespace Propfull.AspNet.Config
{
    public class ConfigService<TConfig> where TConfig : class, new()
    {
        private readonly TConfig config;

        public ConfigService(IOptions<TConfig> configOptions)
        {
            config = configOptions.Value;
        }
    }
}