using Xunit;
using DefaultDomain;
using DefaultDomain.Classes;

namespace DefaultDomainTests
{
    public class DefaultDomainTests
    {
        [Fact]
        public async void TestMethodTest()
        {
            Main temp = new Main();

            Gebruiker x = await temp.Test();
            Assert.NotNull(x);
        }
    }
}