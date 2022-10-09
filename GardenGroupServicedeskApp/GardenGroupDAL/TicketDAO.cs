using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GardenGroupModel;
using MongoDB.Driver;
using MongoDB.Bson;

namespace GardenGroupDAL
{
    public class TicketDAO
    {
        MongoClientInstance mongoClientInstance = MongoClientInstance.GetClientInstance(); 
        

        private string databaseName = "TicketSystemDB";
        private string collectionName = "User";

        public List<Ticket> FillTicketList(string username)
        {
            try
            {
                IMongoDatabase database = mongoClientInstance.Client.GetDatabase("Ticket");
                IMongoCollection<BsonDocument> collection = database.GetCollection<BsonDocument>("TicketSystemDB");

                FilterDefinition<BsonDocument> filter = Builders<BsonDocument>.Filter.Eq("UserName", username);
                List<BsonDocument> documents = collection.Find(filter).ToList();
                List<Ticket> tickets = new List<Ticket>();
                foreach (BsonDocument document in documents)
                {
                    //tickets.Add(AdapterPatternTicket(document))
                }
                return tickets;
            }
            catch (Exception ex)
            {
                throw ex;
            }  
        }
    }
}
