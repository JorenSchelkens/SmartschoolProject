using System.Collections.Generic;

namespace WinkelDomain
{
    public class Winkel
    {
        public int winkelnr;
        public string naam;
        public string beheerder;
        public bool actief;
        public List<Artikel> artikels;
        public bool goedgekeurd;

        public Winkel() 
        {
        }
        public Winkel(string beheerder, string naam)
        {
            this.naam = naam;
            this.beheerder = beheerder;
            artikels = new List<Artikel>();
            goedgekeurd = false;
        }

        public void veranderActief()
        {
            this.actief = !actief;
        }
    }
}