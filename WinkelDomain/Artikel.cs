using System;
namespace WinkelDomain
{
    public class Artikel
    {
        public int productnr;
        public double standaardPrijs;
        public string productnaam;
        public int winkelnr;
        public int stock;
        public int korting;
        public bool actief;

        public Artikel()
        {

        }
        public Artikel(string productnaam)
        {
            this.productnaam = productnaam;
        }
        public void kortingAanzetten(int korting)
        {
            this.korting = korting;
        }
        public double geefHuidigePrijs()
        {
            //TODO
            return standaardPrijs;
        }
    }
}