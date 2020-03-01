using System;
using SmartschoolWSDL;
using System.Threading.Tasks;

namespace DefaultDomain.Classes
{
    public class SmartschoolWSDLAccess
    {
        private const string AccessCode = "83cc8503c160701a7aa4";
        private V3PortClient V3PortClient { get; set; } = new V3PortClient();

        public SmartschoolWSDLAccess()
        {
        }

        public async Task<string> GetUserDetailsByUsername(string username)
        {
            return await this.V3PortClient.getUserDetailsByUsernameAsync(AccessCode, username);
        }

        public async Task<string> GetUserClass(string gebruikersnaam)
        {
            string temp = "";

            try
            {
                temp = await this.V3PortClient.getUserOfficialClassAsync(AccessCode, gebruikersnaam, "2020-02-4");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            return temp;
        }
    }
}