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
    public class LoginDAO
    {
        private IMongoCollection<BsonDocument> collection = null;
        private IMongoDatabase database = null;

        private string databaseName = "TicketSystemDB";
        private string collectionName = "User";

        public string GetPassword(string username, MongoClient client)
        {
            try
            {
                database = client.GetDatabase(databaseName);
                collection = database.GetCollection<BsonDocument>(collectionName);
                //filter                
                var filter = Builders<BsonDocument>.Filter.Empty;
                var sorted = collection.Find(filter).Sort("{Username:1}");//hmm waarschijnlijk anders noemen

                FilterDefinition<BsonDocument> filter2 = Builders<BsonDocument>.Filter.Eq("UserName", username);
                BsonDocument document = collection.Find(filter2).First();
                //query
                                
                //var course = collection.FindAs<User>(MongoDB.Driver.Builders.Query.EQ("Title", "Todays Course")).SetFields(Fields.Include("Title", "Description").Exclude("_id")).ToList();

                if (document.IsBsonNull)
                {
                    //error detected
                    throw new Exception("incorrect username or password, please make sure you have spelled everything correctly.");
                }
                else
                {
                    //return password
                    return ReadUser(document);
                }
            }
            catch(Exception ex)
            {
                throw new Exception("Data could not be retrieved from the database. Please try again, error: " + ex.Message);
            }

            //Gewenste uitput = password (username is al bekend).

            //Moet ik ook nog een ROLE meegeven? of pas nadat het gevalideerd is. Kan dubbele code vermijden om gewoon meteen op te halen.
            //ANSWER: Nee, maar nadat het wachtwoord is gevalideerd en voordat het juiste scherm tevoorschijn komt moet eigenlijk een singleton van user("UserInstance.cs" in model) worden geinstantieerd die dan de ingelogde gebruiker representeert.
            //NOTE: Dus in de bovenstaande stap is dan ook een query vanuit het login gedeelte nodig waarin de User die zojuist is gevalideerd wordt opgehaald uit de DB en in zijn geheel wordt meegegeven aan de initiatie van de van "UserInstance".
            
        }

        // \/\/Waarschijnlijk niet nodig\/\/ \\
        private string ReadUser(BsonDocument document)
        {
            //filter document to only get password
            try
            {
                string password = document.GetValue("Password").ToString();

                return password; //Sorry nog heel even wachten. ja doe maar sem community. Wat?
            }
            catch (Exception ex)
            {
                throw new Exception("Data could not be retrieved from the database. Please try again, error: " + ex.Message);
            }            
        }
    }
}
