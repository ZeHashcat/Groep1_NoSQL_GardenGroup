using GardenGroupModel;
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

        /// <summary>
        /// 
        /// Writen by Floortje 
        /// 
        /// gets all tickets from the database
        /// </summary>
        /// <returns></returns>
        public  List<BsonDocument> ReadAsync()
        {
            //List<BsonDocument> document = collection.Find("{ }").ToList();

            var aggregate = collection.Aggregate().Match(new BsonDocument

            {
            { "_id", 1 },
            { "UserName", "ZeHashcat" },
            { "Status", "open" },
            { "Ticket Author", "Shreck" },
            { "DateReported", "09/10/22" },
            { "DeadLine", "09/10/22" },
            { "Description", "Donkey is trying to vacinate the laptops against computer viruses again" },
            { "Incident", "service" },
            { "Subject", "Vacinatie" },
            { "impact", "high" },
            { "urgency", "low" }
        }).Lookup("User", "UserName", "Username", "User");
            List<BsonDocument> document =  aggregate.ToList();

            return document;
            
        }
        /// <summary>
        ///  writen by Floortje
        ///  gets all tickets by a filter of <paramref name="ticket"/>
        /// </summary>
        /// <param name="ticket"></param>
        /// <returns></returns>
        public List<BsonDocument> Read(Ticket ticket)
        {
             
            BsonDocument DocumentToFind = ticket.ToBsonDocument();
           
            List<BsonDocument> document = collection.Find(DocumentToFind).ToList();

            return document;

        }
    }
}

