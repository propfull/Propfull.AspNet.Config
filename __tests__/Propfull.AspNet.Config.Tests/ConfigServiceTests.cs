using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Xunit;

namespace Propfull.AspNet.Config.Tests
{
    public class ConfigServiceTests
    {
        class SampleConfig
        {
            public string Name { get; set; }
            public int Version { get; set; }
        }

        [Fact(DisplayName = "Should return a clone of the config")]
        public async Task ShouldReturnACloneOfTheConfigTest()
        {
            var config = new SampleConfig
            {
                Name = "sample name",
                Version = 1
            };

            var configOptions = Options.Create(config);
            var sut = new ConfigService<SampleConfig>(configOptions);


            var actualConfig = await sut.GetConfigAsync();

            // Changing the returned config should not affect the service's config
            actualConfig.Version = 2;

            Assert.Equal(1, config.Version);
        }
    }
}