using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using GardenGroupDAL;
using GardenGroupModel;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Driver;

namespace GardenGroupDAL
{
    public class RoleDAO
    {
        public async Task<List<string>> GetRoles()
        {
            MongoClientInstance mongoClientInstance = MongoClientInstance.GetClientInstance();
            IMongoDatabase database = mongoClientInstance.Client.GetDatabase("TicketSystemDB");
            IMongoCollection<BsonDocument> collection = database.GetCollection<BsonDocument>("Role");

            List<string> roles = new List<string>();

            //using async becouse of the await. This is necessary.
            List<BsonDocument> documents = await collection.Find(new BsonDocument()).ToListAsync();

            foreach (var document in documents)
            {
                string role = document.GetValue("_id").ToString();
                roles.Add(role);
            }
            return roles;
        }
    }
}
