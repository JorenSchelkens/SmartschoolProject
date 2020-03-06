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
            GebruikerInfo temp;

            string userString = await smartschoolWSDLAccess.GetUserDetailsByUsername(email.Split('@')[0]);

            try
            {
                temp = JObject.Parse(userString).ToObject<GebruikerInfo>();
                temp.SyncVars();
            }
            catch (Exception e)
            {
                temp = null;
                Console.WriteLine(e);
            }

            return temp;
        }
    }
}