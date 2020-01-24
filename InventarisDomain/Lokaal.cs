using System;
using System.Collections.Generic;

namespace InventarisDomain
{
    public class Lokaal
    {
        public string klasVerantwoordelijke { get; set; }
        public int lokaalNr { get; set; }
        public int aantalBanken { get; set; } = 0;
        public int aantalBeamers { get; set; } = 0;
        public int aantalStoelen { get; set; } = 0;
        public int aantalComputers { get; set; } = 0;
        public int aantalSchermen { get; set; } = 0;

        public void voegBankToe()
        {
            aantalBanken++;
        }

        public void verwijderBank()
        {
            aantalBanken--;
        }


    }
}
