using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Options;
using Propfull.AspNet.Config.Exceptions;

namespace Propfull.AspNet.Config
{
    public static class IServiceProviderExtensions
    {
        public static ConfigService<T> GetConfigService<T>(this IServiceProvider factory)
         where T : class, new()
        {
            var options = factory.GetService(typeof(IOptions<T>)) as IOptions<T>;

            var validationContext = new ValidationContext(options.Value);
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(
                options.Value,
                validationContext,
                validationResults,
                true);

            if (!isValid) throw new InvalidConfigException(validationResults);

            return new ConfigService<T>(options);
        }
    }
}