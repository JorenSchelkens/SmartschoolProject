using System.Collections.Generic;

namespace DefaultDomain.Classes
{
    public class GebruikerInfo
    {
        public string Voornaam { get; set; }
        public string Naam { get; set; }
        public string GebruikersNaam { get; set; }
        public string Status { get; set; }
        public string Geslacht { get; set; }
        public List<Group> Groups { get; set; }

        public bool IsAdmin { get; set; }
        public bool IsLeerling { get; set; }

        //Transactie

        public GebruikerInfo()
        {
            this.Groups = new List<Group>();
        }

        public void SyncVars()
        {
            this.IsLeerling = (this.Groups[0].isKlas) ? true : false;

            // TODO Admin
        }
    }
}