using System;

namespace WinkelDomain
{
    public class BesteldArtikel
    {
        public int Productnr { get; set; }
        public double Prijs { get; set; }
        public string Productnaam { get; set; }
        public int Aantal { get; set; }
        public string Notitie { get; set; } = "";
        public string AantalString = "";
        private Artikel artikel;

        public BesteldArtikel(Artikel artikel, int aantal, string notitie)
        {
            this.artikel = artikel;
            this.Productnr = artikel.productnr;
            this.Prijs = Math.Round(artikel.geefHuidigePrijs() * aantal, 2);
            this.Productnaam = artikel.productnaam;

            this.Aantal = aantal;
            this.Notitie = (notitie != null) ? notitie : "";

            this.AantalString = $"{this.Aantal}x";
        }

        public void ResetAantalString()
        {
            this.AantalString = $"{this.Aantal}x";
        }

        public int GetWinkelNr()
        {
            if(artikel != null)
            {
                return artikel.winkelnr;
            }
            else
            {
                return -1;
            }
        }

        public string PrettyPrint()
        {
            string temp = $"{this.AantalString} {this.Productnaam}";

            if (!string.IsNullOrWhiteSpace(this.Notitie))
            {
                temp += $" (notitie: {this.Notitie})";
            }

            return temp;
        }
    }
}