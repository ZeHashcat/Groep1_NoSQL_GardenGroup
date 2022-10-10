using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GardenGroupModel;
using MongoDB.Bson;
using MongoDB.Driver;

namespace GardenGroupDAL
{
    public class UserDAO
    {
        private IMongoCollection<BsonDocument> collection = null;
        private IMongoDatabase database = null;
        MongoClientInstance mongoClientInstance = MongoClientInstance.GetClientInstance();

        private string databaseName = "TicketSystemDB";
        private string collectionName = "User";

        public HashWithSaltResult GetPassword(string username)
        {
            try
            {               
                database = mongoClientInstance.Client.GetDatabase(databaseName);
                collection = database.GetCollection<BsonDocument>(collectionName);
                
                FilterDefinition<BsonDocument> filter = Builders<BsonDocument>.Filter.Eq("Username", username);
                BsonDocument document = collection.Find(filter).FirstOrDefault();
                //query

                //var course = collection.FindAs<User>(MongoDB.Driver.Builders.Query.EQ("Title", "Todays Course")).SetFields(Fields.Include("Title", "Description").Exclude("_id")).ToList();

                if (document == null)
                {
                    //error detected
                    throw new Exception("incorrect username or password, please make sure you spelled everything correctly.");
                }
                else
                {
                    //return password
                    return ReadPassword(document);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Data could not be retrieved from the database. Please try again, error: " + ex.Message);
            }

            //Gewenste uitput = password (username is al bekend).

            //Moet ik ook nog een ROLE meegeven? of pas nadat het gevalideerd is. Kan dubbele code vermijden om gewoon meteen op te halen.
            //ANSWER: Nee, maar nadat het wachtwoord is gevalideerd en voordat het juiste scherm tevoorschijn komt moet eigenlijk een singleton van user("UserInstance.cs" in model) worden geinstantieerd die dan de ingelogde gebruiker representeert.
            //NOTE: Dus in de bovenstaande stap is dan ook een query vanuit het login gedeelte nodig waarin de User die zojuist is gevalideerd wordt opgehaald uit de DB en in zijn geheel wordt meegegeven aan de initiatie van de van "UserInstance".

        }

        // \/\/Waarschijnlijk niet nodig\/\/ \\
        private HashWithSaltResult ReadPassword(BsonDocument document)
        {
            //filter document to only get password
            try
            {
                string hashedPassword = document.GetValue("Password").ToString();
                string salt = document.GetValue("Salt").ToString();
                HashWithSaltResult hashAndSaltResult = new HashWithSaltResult(salt, hashedPassword);
                
                return hashAndSaltResult;
            }
            catch (Exception ex)
            {
                throw new Exception("Data could not be retrieved from the database. Please try again, error: " + ex.Message);
            }            
        }

        public BsonDocument GetUser(string username)
        {
            try
            {                
                database = mongoClientInstance.Client.GetDatabase(databaseName);
                collection = database.GetCollection<BsonDocument>(collectionName);

                FilterDefinition<BsonDocument> filter = Builders<BsonDocument>.Filter.Eq("Username", username);
                BsonDocument document = collection.Find(filter).First();
                //query

                //var course = collection.FindAs<User>(MongoDB.Driver.Builders.Query.EQ("Title", "Todays Course")).SetFields(Fields.Include("Title", "Description").Exclude("_id")).ToList();

                if (document.IsBsonNull)
                {
                    //error detected
                    throw new Exception("Incorrect username or password, please make sure you have spelled everything correctly.");
                }
                else
                {
                    //return password
                    return document;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Data could not be retrieved from the database. Please try again, error: " + ex.Message);
            }
        }

        public string ValidateEmail(string email)
        {
            try
            {                
                database = mongoClientInstance.Client.GetDatabase(databaseName);
                collection = database.GetCollection<BsonDocument>(collectionName);

                FilterDefinition<BsonDocument> filter = Builders<BsonDocument>.Filter.Eq("E-Mail", email);
                BsonDocument document = collection.Find(filter).FirstOrDefault();
                //query

                //var course = collection.FindAs<User>(MongoDB.Driver.Builders.Query.EQ("Title", "Todays Course")).SetFields(Fields.Include("Title", "Description").Exclude("_id")).ToList();

                if (document == null)
                {
                    //error detected
                    throw new Exception("Email cannot be found in our system, please make sure you spelled everything correctly.");
                }
                else
                {
                    //return email
                    return GetEmail(document);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Data could not be retrieved from the database. Please try again, error: " + ex.Message);
            }
        }

        private string GetEmail(BsonDocument document)
        {
            try
            {
                string email = document.GetValue("E-Mail").ToString();               
               
                return email;
            }
            catch (Exception ex)
            {
                throw new Exception("Data could not be retrieved from the database. Please try again, error: " + ex.Message);
            }
        }
    }
}
