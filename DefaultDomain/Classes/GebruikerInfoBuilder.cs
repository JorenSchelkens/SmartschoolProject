using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace DefaultDomain.Classes
{
    public static class GebruikerInfoBuilder
    {
        public static async Task<GebruikerInfo> BuildGebruikerInfoFromEmail(string email)
        {
            SmartschoolWSDLAccess smartschoolWSDLAccess = new SmartschoolWSDLAccess();
            GebruikerInfo temp = new GebruikerInfo();

            string userString = await smartschoolWSDLAccess.GetUserDetailsByUsername(email.Split('@')[0]);

            //temp = JsonConvert.DeserializeObject<GebruikerInfo>(userString);

            temp = JObject.Parse(userString).ToObject<GebruikerInfo>();

            return temp;
        }
    }
}