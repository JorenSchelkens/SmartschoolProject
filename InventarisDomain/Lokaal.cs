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

        public void voegBeamerToe()
        {
            aantalBeamers++;
        }

        public void verwijderBeamer()
        {
            aantalBeamers--;
        }

        public void voegStoelToe()
        {
            aantalStoelen++;
        }

        public void verwijderStoel()
        {
            aantalStoelen--;
        }

        public void voegComputerToe()
        {
            aantalComputers++;
        }

        public void verwijderComputer()
        {
            aantalComputers--;
        }

        public void voegSchermToe()
        {
            aantalSchermen++;
        }

        public void verwijderScherm()
        {
            aantalSchermen--;
        }




    }
}
