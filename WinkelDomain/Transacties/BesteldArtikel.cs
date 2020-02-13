namespace WinkelDomain
{
    public class BesteldArtikel
    {
        public int Productnr { get; set; }
        public double Prijs { get; set; }
        public string Productnaam { get; set; }
        public int Aantal { get; set; }
        public string Notitie { get; set; } = "";

        public BesteldArtikel(Artikel artikel, int aantal, string notitie)
        {
            this.Productnr = artikel.productnr;
            this.Prijs = artikel.geefHuidigePrijs();
            this.Productnaam = artikel.productnaam;

            this.Aantal = aantal;
            this.Notitie = (notitie != null) ? notitie : "";
        }
    }
}