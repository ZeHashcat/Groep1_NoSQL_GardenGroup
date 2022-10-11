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
        //static string connstring = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        private IMongoCollection<BsonDocument> collection = null;
        private IMongoDatabase database = null;

        private string databaseName = "TicketSystemDB";
        private string collectionName = "User";

       public TicketDAO(){
            MongoClientInstance mongoClientInstance = MongoClientInstance.GetClientInstance();
                

            //database = mongoClientInstance.Client.GetDatabase(databaseName);
            //collection = database.GetCollection<BsonDocument>(collectionName);
            Console.WriteLine("test");
        }
    }
}
