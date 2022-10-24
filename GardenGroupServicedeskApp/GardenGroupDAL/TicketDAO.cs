using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using GardenGroupModel;
using MongoDB.Driver;
using MongoDB.Bson;

namespace GardenGroupDAL
{
    public class TicketDAO
    {
        static string connstring = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        private IMongoCollection<BsonDocument>? collection = null;
        private IMongoDatabase? database = null;

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
        public  List<BsonDocument> Read()
        {
            //List<BsonDocument> document = collection.Find("{ }").ToList();

            IAggregateFluent<BsonDocument> aggregate = collection.Aggregate().Match(new BsonDocument{
            }).Lookup("User", "UserName", "Username", "User");
            List<BsonDocument> document = aggregate.ToList();

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

            //find single Ticket and its user by agregation

            //transforms ticket object to bsondocument
            BsonDocument DocumentToFind = ticket.ToBsonDocument();
            //delete user
            DocumentToFind.Remove("User");

            //set user name value in place of User
            DocumentToFind.Set("UserName", BsonValue.Create(ticket.User.UserName.Value));

            
            List<BsonDocument> document = collection.Aggregate().Match(DocumentToFind).Lookup("User", "UserName", "Username", "User").Sort(Builders<BsonDocument>.Sort.Descending("_id")).ToList();

            return document;

        }

        /// <summary>
        ///  writen by Floortje
        /// </summary>
        /// <param name="ticket"></param>
        public void Create(Ticket ticket)
        {
            BsonDocument documentToAdd = ticket.ToBsonDocument();
            //delete user
            documentToAdd.Remove("User");

            //set user name value in place of User
            documentToAdd.Set("UserName", BsonValue.Create(ticket.User.UserName.Value));
            try
            {
                collection.InsertOne(documentToAdd);
            }
            catch(MongoWriteException e)
            {

                throw new Exception(e.ToString());
            }
        }

        /// <summary>
        ///  writen by Floortje
        ///  gets all tickets by a filter of <paramref name="ticket"/>
        /// </summary>
        /// <param name="ticket"></param>
        /// <returns></returns>
        public BsonDocument Update(Ticket ticketToUpdate,Ticket Update)
        {
            BsonDocument documentToUpdate = ticketToUpdate.ToBsonDocument();

            BsonDocument updateddocument = Update.ToBsonDocument();


            //delete user
            documentToUpdate.Remove("User");
            updateddocument.Remove("User");


            //set user name value in place of User
            documentToUpdate.Set("UserName", BsonValue.Create(ticketToUpdate.User.UserName.Value));
            updateddocument.Set("UserName", BsonValue.Create(Update.User.UserName.Value));

            FilterDefinition<BsonDocument> filter = documentToUpdate;
            UpdateDefinition<BsonDocument> update = updateddocument;


            BsonDocument returnedDocument = collection.FindOneAndUpdate(filter, update);
          /*  if(!returnedDocument.Equals(updateddocument))
            {
                
                throw new Exception("something went wrong the ticket whas not updated");

            }*/
            return returnedDocument;

        }

        public BsonDocument Delete(Ticket ticket)
        {
            BsonDocument documentToDelete = ticket.ToBsonDocument();
            documentToDelete.Remove("User");
            documentToDelete.Set("UserName", BsonValue.Create(ticket.User.UserName.Value));
            FilterDefinition<BsonDocument> FilterToDelete = documentToDelete;
            BsonDocument documentToValidate = collection.FindOneAndDelete(FilterToDelete);

        /*    if (documentToValidate != documentToDelete)
            {
                throw new Exception("something went wrong the ticket whas not deleted");
            }*/

            return documentToValidate;
        }



    }
}

