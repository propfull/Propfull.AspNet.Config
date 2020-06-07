using Microsoft.Extensions.Options;

namespace Propfull.AspNet.Config.Tests
{
    public class ConfigServiceTests
    {
        class SampleConfig
        {
            public string Name { get; set; }
            public int Version { get; set; }
        }

        private readonly IOptions<SampleConfig> configOptions;
        private readonly ConfigService<SampleConfig> sut;

        public ConfigServiceTests()
        {
            configOptions = Options.Create(new SampleConfig
            {
                Name = "sample name",
                Version = 1
            });

            this.sut = new ConfigService<SampleConfig>(configOptions);
        }
    }
}