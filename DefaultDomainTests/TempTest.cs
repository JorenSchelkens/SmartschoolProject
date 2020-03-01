using DefaultDomain.Classes;
using Xunit;

namespace DefaultDomainTests
{
    public class TempTest
    {
        [Fact]
        public void UserTest()
        {
            Gebruiker gebruiker = new Gebruiker();
            gebruiker.Setup("joren.schelkens@bazandpoort.be");

            Assert.NotNull(gebruiker);
        }
    }
}