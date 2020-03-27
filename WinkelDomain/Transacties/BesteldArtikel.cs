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

        public BesteldArtikel(Artikel artikel, int aantal, string notitie)
        {
            this.Productnr = artikel.productnr;
            this.Prijs = artikel.geefHuidigePrijs() * aantal;
            this.Productnaam = artikel.productnaam;

            this.Aantal = aantal;
            this.Notitie = (notitie != null) ? notitie : "";

            this.AantalString = $"{this.Aantal}x";
        }

        public void ResetAantalString()
        {
            this.AantalString = $"{this.Aantal}x";
        }
    }
}