using System;
using DefaultDomain;
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

        [Fact]
        public void MailTest()
        {
            MailClass mail = new MailClass();

            bool succes = mail.SendMail("joren.schelkens@bazandpoort.be", "JorenSchelkens", Guid.NewGuid().ToString());

            Assert.True(succes);
        }
    }
}