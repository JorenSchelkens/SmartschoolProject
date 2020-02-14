namespace InventarisDomain
{
    public class Voorwerp
    {
        public int voorwerpNr { get; set; }
        public string voorwerpNaam { get; set; }
        public int aantal { get; set; }

        public Voorwerp()
        {

        }

        public Voorwerp(int voorwerpNr, string voorwerpNaam)
        {
            this.voorwerpNr = voorwerpNr;
            this.voorwerpNaam = voorwerpNaam;
        }
    }
}