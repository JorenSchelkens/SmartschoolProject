using System;
namespace WinkelDomain
{
    public class Artikel
    {
        public int productnr;
        public double prijs;
        public string productnaam;
        public int stock;
        public int winkelnr;
        public int korting;
        public bool actief;
        public Artikel() { }
        public void kortingAanzetten(int korting)
        {
            this.korting = korting;
        }
    }
}