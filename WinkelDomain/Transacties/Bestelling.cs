using System;
using System.Collections.Generic;

namespace WinkelDomain
{
    public class Bestelling
    {
        public int BestelNr { get; set; }
        public DateTime AanmaakDatum { get; set; }
        public string GebruikersNaam { get; set; }
        public List<BesteldArtikel> BesteldeArtikels { get; set; }
        public double TotaalBedrag { get; set; } = 0;

        public Bestelling(string gebruikersNaam)
        {
            this.AanmaakDatum = DateTime.Now;
            this.GebruikersNaam = gebruikersNaam;
        }

    }
}