using MongoDB.Driver;
using RealEstate.Properties;

namespace RealEstate.Rentals
{
    public class RealEstateContext
    {
        private readonly MongoDatabase _database;
        public RealEstateContext()
        {
            var client = new MongoClient(Settings.Default.RealEstateConnectionString);
            var server = client.GetServer();
            _database = server.GetDatabase(Settings.Default.RealEstateDatabase);
        }

        public MongoDatabase Database {
            get { return _database; }
        }
    }
}