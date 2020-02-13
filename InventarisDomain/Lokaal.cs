using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace InventarisDomain
{
    public class Lokaal
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement("lokaalVerantwoordelijke")]
        public string lokaalVerantwoordelijke { get; set; }

        [BsonElement("lokaalnr")]
        public int lokaalNr { get; set; }

        [BsonIgnore]
        public int aantalBanken { get; set; } = 0;
        [BsonIgnore]
        public int aantalBeamers { get; set; } = 0;
        [BsonIgnore]
        public int aantalStoelen { get; set; } = 0;
        [BsonIgnore]
        public int aantalComputers { get; set; } = 0;
        [BsonIgnore]
        public int aantalSchermen { get; set; } = 0;

        [BsonElement("voorwerpen")]
        public List<Voorwerp> Voorwerpen { get; set; } = new List<Voorwerp>();

        [BsonElement("actief")]
        public bool isActief { get; set; }

        public Lokaal()
        {

        }
        public Lokaal(int lokaalNr)
        {
            this.lokaalNr = lokaalNr;
        }

        public void voegBankToe()
        {
            aantalBanken++;
        }

        public void verwijderBank()
        {
            if(aantalBanken > 0)
            {
                aantalBanken--;
            }
        }

        public void voegBeamerToe()
        {
            aantalBeamers++;
        }

        public void verwijderBeamer()
        {
            if (aantalBeamers > 0)
            {
                aantalBeamers--; 
            }
            
        }

        public void voegStoelToe()
        {
            aantalStoelen++;
        }

        public void verwijderStoel()
        {
            if(aantalStoelen > 0)
            {
                aantalStoelen--;
            }
        }

        public void voegComputerToe()
        {
            aantalComputers++;
        }

        public void verwijderComputer()
        {
            if(aantalComputers > 0)
            {
                aantalComputers--;
            }
        }

        public void voegSchermToe()
        {
            aantalSchermen++;
        }

        public void verwijderScherm()
        {
            if(aantalSchermen > 0)
            {
                aantalSchermen--;
            }
        }
    }
}