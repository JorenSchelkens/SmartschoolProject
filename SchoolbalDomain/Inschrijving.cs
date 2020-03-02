namespace SchoolbalDomain
{
    public class Inschrijving
    {
        public string naam { get; set; }
        public string klas { get; set; }
        public string gast1 { get; set; }
        public string gast2 { get; set; }
        public Inschrijving(string naam, string klas)
        {
            this.naam = naam;
            this.klas = klas;
        }
        public Inschrijving(string naam, string klas, string gast1)
        {
            this.naam = naam;
            this.klas = klas;
            this.gast1 = gast1;
        }

        public Inschrijving(string naam, string klas, string gast1, string gast2)
        {
            this.naam = naam;
            this.klas = klas;
            this.gast1 = gast1;
            this.gast2 = gast2;
        }
    }
}
