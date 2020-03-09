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
    }
}