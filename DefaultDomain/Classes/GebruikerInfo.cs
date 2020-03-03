using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace DefaultDomain.Classes
{
    public class GebruikerInfo
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

        public GebruikerInfo()
        {
            this.SmartschoolWSDLAccess = new SmartschoolWSDLAccess();
        }

        public async Task<GebruikerInfo> Initialize(string email)
        {
            return await this.GetGebruikerDetails(email);
        }

        private async Task<GebruikerInfo> GetGebruikerDetails(string email)
        {
            string userString = await this.SmartschoolWSDLAccess.GetUserDetailsByUsername(email.Split('@')[0]);

            GebruikerInfo gebruikerInfo = JObject.Parse(userString).ToObject<GebruikerInfo>();

            return gebruikerInfo;
        }
    }
}