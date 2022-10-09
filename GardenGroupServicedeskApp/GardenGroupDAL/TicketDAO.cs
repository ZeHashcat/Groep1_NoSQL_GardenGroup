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
        

        

        public List<ICollectionObject> FillTicketList(string username)
        {
            string databaseName = "TicketSystemDB";
            string collectionName = "Ticket";
            try
            {
                IMongoDatabase database = mongoClientInstance.Client.GetDatabase(databaseName);
                IMongoCollection<BsonDocument> collection = database.GetCollection<BsonDocument>(collectionName);

                FilterDefinition<BsonDocument> filter = Builders<BsonDocument>.Filter.Eq("UserName", username);
                List<BsonDocument> documents = collection.Find(filter).ToList();
                List<ICollectionObject> tickets = new List<ICollectionObject>();

                foreach (BsonDocument document in documents)
                {
                    Ticket ticket = new Ticket(
                        int.Parse(document.GetValue("_id").ToString()),
                        document.GetValue("Status").ToString(),
                        document.GetValue("Ticket Author").ToString(),
                        document.GetValue("DateTime Created").ToString()
                        );
                    
                    tickets.Add(ticket);
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
