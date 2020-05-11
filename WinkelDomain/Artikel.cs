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

        public double geefHuidigePrijs()
        {
            if (korting == 0)
            {
                return Math.Round(standaardPrijs, 2);
            }
            else
            {
                return Math.Round(standaardPrijs * ((100 - korting) / 100.00), 2);
            }
        }
    }
}