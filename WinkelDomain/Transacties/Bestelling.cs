using System;
using System.Collections.Generic;

namespace WinkelDomain
{
    public class Bestelling
    {
        public int BestelNr { get; set; }
        public DateTime AanmaakDatum { get; set; }
        public string GebruikersNaam { get; set; }
        private List<BesteldArtikel> BesteldeArtikels { get; set; }
        public double TotaalBedrag { get; set; } = 0;
        public string Code { get; set; }

        public Bestelling(string gebruikersNaam)
        {
            this.AanmaakDatum = DateTime.Now;
            this.GebruikersNaam = gebruikersNaam;

            this.Code = Guid.NewGuid().ToString();

            this.BesteldeArtikels = new List<BesteldArtikel>();
        }

        public List<BesteldArtikel> GetBesteldeArtikels()
        {
            return this.BesteldeArtikels;
        }

        public bool AddArtikel(BesteldArtikel besteldArtikel)
        {
            foreach (BesteldArtikel temp in this.BesteldeArtikels)
            {
                if (temp.Productnr == besteldArtikel.Productnr)
                {
                    return false;
                }
            }

            this.BesteldeArtikels.Add(besteldArtikel);

            return true;
        }

        public bool IsBestellingEmpty()
        {
            if(this.BesteldeArtikels.Count == 0)
            {
                return true;
            }

            return false;
        }

        public void VerwijderArtikel(BesteldArtikel besteldArtikel)
        {   
            if(besteldArtikel.Aantal > 1)
            {
                besteldArtikel.Aantal--;
                besteldArtikel.ResetAantalString();
            }
            else
            {
                this.BesteldeArtikels.Remove(besteldArtikel);
            }
        }

        public void FinishBestelling()
        {
            this.TotaalBedrag = 0;

            foreach (BesteldArtikel besteldArtikel in this.BesteldeArtikels)
            {
                this.TotaalBedrag += besteldArtikel.Prijs;
            }
        }
    }
}