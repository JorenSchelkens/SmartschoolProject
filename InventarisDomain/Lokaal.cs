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

        
    }
}