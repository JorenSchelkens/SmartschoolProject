using System.Collections.Generic;

namespace InventarisDomain
{
    public class Lokaal
    {
        public string lokaalVerantwoordelijke { get; set; }
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
            if(aantalBanken > 0)
            {
                aantalBanken--;
            }
        }

        public void voegBeamerToe()
        {
            aantalBeamers++;
        }

        public void verwijderBeamer()
        {
            if (aantalBeamers > 0)
            {
                aantalBeamers--; 
            }
            
        }

        public void voegStoelToe()
        {
            aantalStoelen++;
        }

        public void verwijderStoel()
        {
            if(aantalStoelen > 0)
            {
                aantalStoelen--;
            }
        }

        public void voegComputerToe()
        {
            aantalComputers++;
        }

        public void verwijderComputer()
        {
            if(aantalComputers > 0)
            {
                aantalComputers--;
            }
        }

        public void voegSchermToe()
        {
            aantalSchermen++;
        }

        public void verwijderScherm()
        {
            if(aantalSchermen > 0)
            {
                aantalSchermen--;
            }
        }
    }
}