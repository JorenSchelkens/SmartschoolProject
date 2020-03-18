namespace SchoolbalDomain
{
    public class Gast
    {
        public string Naam { get; set; }
        public bool Confirmed { get; set; }

        public Gast(string naam)
        {
            this.Naam = naam;
            this.Confirmed = false;
        }
    }
}