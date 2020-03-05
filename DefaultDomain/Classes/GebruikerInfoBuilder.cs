using Newtonsoft.Json.Linq;
using System;
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

            //Internnummer leerkracht --> null

            try
            {
                temp = JObject.Parse(userString).ToObject<GebruikerInfo>();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return temp;
        }
    }
}