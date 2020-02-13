using System;
namespace WinkelDomain
{
    public class Artikel
    {
        public int productnr;
        public double standaardPrijs;
        public string productnaam;
        public int stock;
        public int korting;
        public bool actief;

        public Artikel()
        {
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