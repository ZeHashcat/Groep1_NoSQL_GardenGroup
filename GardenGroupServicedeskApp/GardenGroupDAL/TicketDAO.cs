using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GardenGroupDAL
{
    public class TicketDAO
    {
        static string connstring = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        private IMongoCollection<BsonDocument> collection = null;
        private IMongoDatabase database = null;

        private string databaseName = "TicketSystemDB";
        private string collectionName = "Ticket";

       public TicketDAO(){
            MongoClientInstance mongoClientInstance = MongoClientInstance.GetClientInstance(connstring);


            database = mongoClientInstance.Client.GetDatabase(databaseName);
            collection = database.GetCollection<BsonDocument>(collectionName);
        }
        public List<BsonDocument> Read()
        {
            FilterDefinition<BsonDocument> filter = Builders<BsonDocument>.Filter.Eq("UserName", "ZeHashcat");
            List<BsonDocument> document = collection.Find(filter).ToList();

            return document;
            
        }
    }
}
