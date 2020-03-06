using DefaultDomain.Classes;
using Xunit;

namespace DefaultDomainTests
{
    public class TempTest
    {
        [Fact]
        public void UserTest()
        {
            GebruikerInfo gebruiker = new GebruikerInfo();
            //gebruiker.Setup("joren.schelkens@bazandpoort.be");

            Assert.NotNull(gebruiker);
        }
    }
}