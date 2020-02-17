namespace InventarisDomain
{
    public class Voorwerp
    {
        public string voorwerpNaam { get; set; }
        public int aantal { get; set; }
        public bool defect { get; set; }
        public int aantalDefecten { get; set; }

        public Voorwerp()
        {

        }

        public Voorwerp(string voorwerpNaam)
        {
            this.voorwerpNaam = voorwerpNaam;
        }

        public void verlaagHoeveelheidVoorwerp()
        {
            aantal--;
        }

        public void verhoogHoeveelheidVoorwerp()
        {
            aantal++;
        }
        public void verlaagDefecte()
        {
            aantalDefecten--;
        }
    }
}