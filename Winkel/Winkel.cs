namespace Winkel
{
    public class Winkel
    {
        public int winkelnr;
        public string naam;
        public string beheerder;
        public bool actief;
        public Winkel() { }
        public void veranderBeheerder(string beheerder)
        {
            this.beheerder = beheerder;
        }
        public void veranderActief()
        {
            this.actief = !actief;
        }
    }
}