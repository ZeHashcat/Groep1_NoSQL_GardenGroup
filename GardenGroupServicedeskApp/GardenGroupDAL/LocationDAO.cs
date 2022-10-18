using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GardenGroupDAL
{
    public class LocationDAO
    {
        public async Task<List<string>> GetLocations()
        {

            MongoClientInstance mongoClientInstance = MongoClientInstance.GetClientInstance();
            IMongoDatabase database = mongoClientInstance.Client.GetDatabase("TicketSystemDB");
            IMongoCollection<BsonDocument> collection = database.GetCollection<BsonDocument>("Location");

        List<string> locations = new List<string>();

            //using async becouse of the await. This is necessary.
            List<BsonDocument> documents = await collection.Find(new BsonDocument()).ToListAsync();

            foreach (var document in documents)
            {
                string location = document.GetValue("Location").ToString();
                locations.Add(location);
            }
            return locations;
        }
    }
}
