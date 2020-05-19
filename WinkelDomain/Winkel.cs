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
        public string adress;

        public Winkel() 
        {
            artikels = new List<Artikel>();
        }

        public Winkel(string beheerder, string naam)
        {
            this.naam = naam;
            this.beheerder = beheerder;
            artikels = new List<Artikel>();
            goedgekeurd = false;
            this.adress = this.naam.Trim().Replace(' ', '-');
        }

        public void veranderActief()
        {
            this.actief = !actief;
        }
    }
}