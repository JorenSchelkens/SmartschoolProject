﻿using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using SmartschoolWSDL;
using DefaultDomain.Classes;

namespace DefaultDomain
{
    public class Main
    {
        public async Task<Gebruiker> Test()
        {
            V3PortClient v3PortClient = new V3PortClient();

            string x = await v3PortClient.getUserDetailsByUsernameAsync("83cc8503c160701a7aa4", "joren.schelkens");
            Gebruiker temp = JObject.Parse(x).ToObject<Gebruiker>();

            return temp;
        }
    }
}