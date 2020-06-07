using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Moq;
using Propfull.AspNet.Config.Exceptions;
using Xunit;

namespace Propfull.AspNet.Config.Tests
{
    public class IServiceProviderExtensionsTests
    {
        public class SampleConfig
        {

            [Required]
            public string Name { get; set; }

            [MaxLength(10)]
            public string Description { get; set; }
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
                Description = "a sample config's description"
            }));
        }

        [Fact(DisplayName = "Should throw InvalidConfigException when registering")]
        public void ShouldThrowInvalidConfigExceptionWhenRegistering()
        {
            var subject = new Func<ConfigService<SampleConfig>>(() => mockServiceProvider
                .Object
                .GetConfigService<SampleConfig>());

            subject
                .Should()
                .Throw<InvalidConfigException>()
                .WithMessage("The config provided contains invalid properties.");
        }

        [Fact(DisplayName = "Should throw InvalidConfigException info if config is invalid when registering")]
        public void ShouldThrowErrorInfoIfConfigIsInvalidWhenRegistering()
        {
            var expectedErrors = new Dictionary<string, string> {
                { "Name", "The Name field is required."},
                { "Description", "The field Description must be a string or array type with a maximum length of '10'."}
            };

            try
            {
                mockServiceProvider
                .Object
                .GetConfigService<SampleConfig>();
            }
            catch (InvalidConfigException e)
            {
                var validationResults = e.Data;

                expectedErrors
                    .Keys
                    .ToList()
                    .ForEach(key =>
                    {
                        validationResults
                            .Any(p => p.MemberNames.Contains(key))
                            .Should()
                            .BeTrue();

                        var result = validationResults.First(p => p.MemberNames.Contains(key));

                        result
                            .ErrorMessage
                            .Should()
                            .Be(expectedErrors[key]);
                    });
            }
        }
    }
}