using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Security.Authentication;
using System.Threading.Tasks;
using InventarisDomain;
using System.Linq;

namespace DefaultDomain
{
    public class DBConnectionMongoDB
    {
        private const string ConnectionString = @"mongodb://localhost";
        private MongoClientSettings MongoClientSettings { get; set; }
        private MongoClient MongoClient { get; set; }
        private IMongoDatabase MongoDatabase { get; set; }
        private IMongoCollection<Lokaal> LokalenCollection { get; set; }

        public DBConnectionMongoDB()
        {
            this.MongoClientSettings = MongoClientSettings.FromUrl(new MongoUrl(ConnectionString));
            this.MongoClientSettings.SslSettings = new SslSettings() { EnabledSslProtocols = SslProtocols.Tls12 };

            this.MongoClient = new MongoClient(this.MongoClientSettings);
            this.MongoDatabase = this.MongoClient.GetDatabase("smproject");
            this.LokalenCollection = this.MongoDatabase.GetCollection<Lokaal>("lokalen");
        }

        public async Task<bool> SaveLokaal(Lokaal lokaal)
        {
            var filter = Builders<Lokaal>.Filter.Eq("lokaalnr", lokaal.lokaalNr);
            var result = await this.LokalenCollection.Find(filter).FirstOrDefaultAsync();

            if (result == null)
            {
                await this.LokalenCollection.InsertOneAsync(lokaal);
                return true;
            }

            return false;
        }

        public async Task<List<Lokaal>> GetAllLokalen()
        {
            return await this.LokalenCollection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task<List<Lokaal>> GetAllLokalenMetDefect()
        {
            List<Lokaal> lokalen = await this.LokalenCollection.Find(new BsonDocument()).ToListAsync();
            List<Lokaal> returnLokalen = new List<Lokaal>();

            foreach (var lokaal in lokalen)
            {
                if (lokaal.Voorwerpen.Any(v => v.defect))
                {
                    returnLokalen.Add(lokaal);
                }
            }

            return returnLokalen;
        }

        public async Task<Lokaal> GetLokaal(int lokaalnr)
        {
            var filter = Builders<Lokaal>.Filter.Eq("lokaalnr", lokaalnr);
            var result = await this.LokalenCollection.Find(filter).FirstOrDefaultAsync();

            return result;
        }

        public async Task<bool> UpdateLokaal(Lokaal Lokaal)
        {
            var result = await this.LokalenCollection.ReplaceOneAsync((v => v.Id == Lokaal.Id), Lokaal);

            return result.ModifiedCount == 1;
        }

        public async Task<bool> ChangeStatusLokaal(Lokaal Lokaal)
        {
            var filter = Builders<Lokaal>.Filter.Eq("_id", Lokaal.Id);

            string temp = (Lokaal.isActief) ? "false" : "true"; 

            var update = Builders<Lokaal>.Update.Set("actief", temp);
            var result = await this.LokalenCollection.UpdateOneAsync(filter, update);

            return result.ModifiedCount == 1;
        }
    }
}