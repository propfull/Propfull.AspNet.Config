# Propfull.AspNet.Config

A simple ConfigService for ASP.NET Core for strongly typed Configuration.

## Features 

- Strongly typed configuration
- Validate config with DataAnnotations

## How to use?

### Setup config class

Create a class for defining the shape of the config.

Example:
```
public class ApiConfig
{
    public string ApiName { get; set; }
    public string ApiVersion { get; set; }
    public string ApiDescription { get; set; }
}
```

### Inject the config

You can inject the config to ASP.NET Core's configuration through any method - appSettings, ENV, etc,.

Example: here we are injecting the config through appSettings, by appending a new section into the valid environment, `appsettings.Development.json` in this case.

```
{
...
    "ApiConfig": {
        "Name": "Propfull Sample API",
        "Version": "v0.0.0-alpha.1",
        "Description": "A sample API to show how to use the ConfigService."
    }
...
}
```

### Bind the config from Configuration to ASP.NET Core's option system

In your ConfigureServices method in [Startup.cs](/samples/Propfull.AspNet.Config.Sample/Startup.cs), call the configure method to bind the options. For example:

```
 services.Configure<ApiConfig>(
     Configuration.GetSection(nameof(ApiConfig)));
```

### Use DI to use the ConfigService

In your ConfigureServices method in [Startup.cs](/samples/Propfull.AspNet.Config.Sample/Startup.cs), call the AddSingleton method to inject the ConfigService through DI

```
services.AddSingleton(
    options => options.GetConfigService<ApiConfig>());
```

### Use the config Service in your code through DI

The ConfigService can be injected like any other Service through DI.

For example, here's a controller that returns the config as a response.

```
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
```

