using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace DefaultDomain.Classes
{
    public class Gebruiker
    {
        private SmartschoolWSDLAccess SmartschoolWSDLAccess { get; set; }

        public string Voornaam { get; set; }
        public string Naam { get; set; }
        public string GebruikersNaam { get; set; }
        public int InternNummer { get; set; }
        public string Status { get; set; }
        public string Geslacht { get; set; }
        public string Klas { get; set; }

        //Transactie

        public Gebruiker()
        {
            this.SmartschoolWSDLAccess = new SmartschoolWSDLAccess();
        }

        public async void Setup(string email)
        {
            await this.GetUserDetails(email);
        }

        private async Task GetUserDetails(string email)
        {
            string userString = await this.SmartschoolWSDLAccess.GetUserDetailsByUsername(email.Split('@')[0]);
            this.TransferData(JObject.Parse(userString).ToObject<Gebruiker>());
        }

        private async void TransferData(Gebruiker gebruiker)
        {
            string temp = await this.SmartschoolWSDLAccess.GetUserClass(gebruiker.GebruikersNaam);
            int i = 0;
        }
    }
}