using System;
using Microsoft.Extensions.Options;

namespace Propfull.AspNet.Config
{
    public static class IServiceProviderExtensions
    {
        public static ConfigService<T> GetConfigService<T>(this IServiceProvider factory)
         where T: class, new()
        {
            var options = factory.GetService(typeof(IOptions<T>)) as IOptions<T>;

            // TODO: run validation
            return new ConfigService<T>(options);
        }
    }
}