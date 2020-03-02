namespace SchoolbalDomain
{
    public class Inschrijving
    {   
        public Gast gastheer { get; set; }
        public string klas { get; set; }
        public Gast gast1 { get; set; }
        public Gast gast2 { get; set; }
            
        public Inschrijving(Gast gastheer, string klas)
        {
            this.gastheer = gastheer;
            this.klas = klas;
        }

    }
}
