using System;
using System.ComponentModel.DataAnnotations;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace Propfull.AspNet.Config.Tests
{
    public class IServiceProviderExtensionsTests
    {
        public class SampleConfig
        {

            [Required]
            public string Name { get; set; }
            public int Version { get; set; }
        }

        private readonly Mock<IServiceProvider> mockServiceProvider;

        public IServiceProviderExtensionsTests()
        {
            mockServiceProvider = new Mock<IServiceProvider>();

            mockServiceProvider
            .Setup(p => p.GetService(typeof(IOptions<SampleConfig>)))
            .Returns(Options.Create(new SampleConfig
            {
                Version = 1
            }));
        }

        [Fact(DisplayName = "Should throw error if config is invalid when registering")]
        public void ShouldThrowErrorIfConfigIsInvalidWhenRegistering()
        {
            var subject = new Func<ConfigService<SampleConfig>>(() => mockServiceProvider
                .Object
                .GetConfigService<SampleConfig>());

            subject.Should().Throw<Exception>();
        }
    }
}