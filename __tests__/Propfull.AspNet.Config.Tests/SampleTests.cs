using Xunit;

namespace Propfull.AspNet.Config.Tests
{
    public class SampleTests
    {
        private readonly Sample sut;

        public SampleTests()
        {
            this.sut = new Sample();
        }

        [Fact]
        public void ShouldGetLibraryNameTest()
        {
            var actualName = sut.GetLibraryName();

            Assert.Equal("Propfull.AspNet.Config", actualName);
        }
    }
}
