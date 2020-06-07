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

        private readonly SampleConfig config;
        private readonly IOptions<SampleConfig> configOptions;
        private readonly ConfigService<SampleConfig> sut;

        public ConfigServiceTests()
        {
            config = new SampleConfig
            {
                Name = "sample name",
                Version = 1
            };

            configOptions = Options.Create(config);
            sut = new ConfigService<SampleConfig>(configOptions);
        }

        [Fact(DisplayName = "Should return a clone of the config")]
        public async Task ShouldReturnACloneOfTheConfigTest()
        {
            var actualConfig = await sut.GetConfigAsync();

            // Changing the returned config should not affect the service's config
            actualConfig.Version = 2;

            Assert.Equal(1, this.config.Version);
        }
    }
}