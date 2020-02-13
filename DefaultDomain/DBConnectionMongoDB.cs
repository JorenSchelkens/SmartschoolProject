using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Security.Authentication;
using System.Threading.Tasks;
using InventarisDomain;

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

        public async Task SaveLokaal(Lokaal lokaal)
        {
            await this.LokalenCollection.InsertOneAsync(lokaal);
        }

        public async Task<List<Lokaal>> GetAllLokalen()
        {
            return await this.LokalenCollection.Find(new BsonDocument()).ToListAsync();
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

        public async Task<bool> DisableLokaal(Lokaal Lokaal)
        {
            var filter = Builders<Lokaal>.Filter.Eq("_id", Lokaal.Id);
            var update = Builders<Lokaal>.Update.Set("actief", "false");

            var result = await this.LokalenCollection.UpdateOneAsync(filter, update);

            return result.ModifiedCount == 1;
        }
    }
}