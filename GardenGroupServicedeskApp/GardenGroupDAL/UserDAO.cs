using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
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
        private IMongoCollection<BsonDocument> collection;
        private IMongoDatabase database;

        private string databaseName = "TicketSystemDB";
        private string collectionName = "User";

        public HashWithSaltResult GetPassword(string username)
        {
            MongoClientInstance mongoClientInstance = MongoClientInstance.GetClientInstance();
                          
            database = mongoClientInstance.Client.GetDatabase(databaseName);
            collection = database.GetCollection<BsonDocument>(collectionName);
                
            FilterDefinition<BsonDocument> filter = Builders<BsonDocument>.Filter.Eq("Username", username);
            BsonDocument document = collection.Find(filter).FirstOrDefault();
            
            if (document == null)
            {
                //error detected
                throw new Exception("Incorrect username or password, please make sure you spelled everything correctly.");
            }
            else
            {
                //return password
                return ReadPassword(document);
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
           
            byte[] hashedPassword = Encoding.ASCII.GetBytes(document.GetValue("Password").ToString());
            byte[] salt = Encoding.ASCII.GetBytes(document.GetValue("Salt").ToString());
            HashWithSaltResult hashAndSaltResult = new HashWithSaltResult(salt, hashedPassword);
                
            return hashAndSaltResult;                             
        }

        public BsonDocument GetUser(string username)
        {
            MongoClientInstance mongoClientInstance = MongoClientInstance.GetClientInstance();
                            
            database = mongoClientInstance.Client.GetDatabase(databaseName);
            collection = database.GetCollection<BsonDocument>(collectionName);

            FilterDefinition<BsonDocument> filter = Builders<BsonDocument>.Filter.Eq("Username", username);
            BsonDocument document = collection.Find(filter).FirstOrDefault();
            
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

        public string ValidateEmail(string email)
        {
            MongoClientInstance mongoClientInstance = MongoClientInstance.GetClientInstance();
                          
            database = mongoClientInstance.Client.GetDatabase(databaseName);
            collection = database.GetCollection<BsonDocument>(collectionName);

            FilterDefinition<BsonDocument> filter = Builders<BsonDocument>.Filter.Eq("E-Mail", email);
            BsonDocument document = collection.Find(filter).FirstOrDefault();
            
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

        private string GetEmail(BsonDocument document)
        {
            
            string email = document.GetValue("E-Mail").ToString();               
               
            return email;            
            
        }

        public void ChangePassword(string email, HashWithSaltResult hashWithSalt)
        {
            MongoClientInstance mongoClientInstance = MongoClientInstance.GetClientInstance();
            
            database = mongoClientInstance.Client.GetDatabase(databaseName);
            collection = database.GetCollection<BsonDocument>(collectionName);

            FilterDefinition<BsonDocument> filter = Builders<BsonDocument>.Filter.Eq("E-Mail", email);
            BsonDocument document = collection.Find(filter).FirstOrDefault();
            //update password
            UpdatePassword(email, document, hashWithSalt);                
                       
        }

        private void UpdatePassword(string email, BsonDocument document, HashWithSaltResult hashWithSalt)
        {
            MongoClientInstance mongoClientInstance = MongoClientInstance.GetClientInstance();
            database = mongoClientInstance.Client.GetDatabase(databaseName);
            collection = database.GetCollection<BsonDocument>(collectionName);

            var update = Builders<BsonDocument>.Update.Set("Password", hashWithSalt.Hash);
            FilterDefinition<BsonDocument> filter = Builders<BsonDocument>.Filter.Eq("E-Mail", email);
            var options = new UpdateOptions { IsUpsert = true };
            collection.UpdateOne(filter, update, options);
                                    
            update = Builders<BsonDocument>.Update.Set("Salt", hashWithSalt.Salt);
            options = new UpdateOptions { IsUpsert = true };
            collection.UpdateOne(filter, update, options);            
        }

        public bool CheckUserData(string username, string email, double phonenumber)
        {
            //Here we will check if any given values wont match the existing ones
            //This is how we find the documents
            MongoClientInstance mongoClientInstance = MongoClientInstance.GetClientInstance();
            database = mongoClientInstance.Client.GetDatabase(databaseName);
            collection = database.GetCollection<BsonDocument>(collectionName);

            var users = collection.Find(new BsonDocument()).ToList();

            foreach (BsonDocument user in users)
            {
                if (user.Contains(username) || user.Contains(email) || user.Contains(phonenumber.ToString()))
                {
                    return false;
                }                    

        }
        //The code below belongs in BsonDocument GetUser(string username)
                FilterDefinition<BsonDocument> filter = Builders<BsonDocument>.Filter.Eq("Username", username);
                BsonDocument document = collection.Find(filter).First();
                //query
                
        //The code below belongs in GetUserData
                //After checking if the values dont match the existing ones in the system we will proceed and add the user
                else
                {
                    return true;
                }                    
            }            
            return false;
        }

        public void AddUser(string username, HashWithSaltResult hashWithSalt, string firstname, string lastname, string email, double phonenumber, string role,  string location)
        {
            MongoClientInstance mongoClientInstance = MongoClientInstance.GetClientInstance();
            database = mongoClientInstance.Client.GetDatabase(databaseName);
            collection = database.GetCollection<BsonDocument>(collectionName);

            BsonDocument doc = new BsonDocument
            {                
            //Adding the data into a document

            //Variabelen aanroepen. En achteraan plaatsen
            { "Username", username},
                {"Password", hashWithSalt.Hash},
                {"Salt", hashWithSalt.Salt},
                {"First Name", firstname},
                {"Last Name", lastname},
                {"E-Mail", email},
                {"Phone Number", phonenumber},
                {"Role", role},
                {"Location", location},
                {"Teams", new BsonDocument() }
            };      
            //Saving the data into the database
            collection.InsertOne(doc);
        }

        public List<ICollectionObject> GetTeam(int? teamNumber = null, string? teamName = null)
        {
            string databaseName = "TicketSystemDB";
            string collectionName = "User";
            MongoClientInstance mongoClientInstance = MongoClientInstance.GetClientInstance();

            IMongoDatabase database = mongoClientInstance.Client.GetDatabase(databaseName);
            IMongoCollection<BsonDocument> collection = database.GetCollection<BsonDocument>(collectionName);

            FilterDefinition<BsonDocument> filter;

            try
            {
                if (teamNumber != null)
                {
                    filter = Builders<BsonDocument>.Filter.Eq("_id", teamNumber);
                }
                else if (teamName != null)
                {
                    filter = Builders<BsonDocument>.Filter.Eq("Name", teamName);
                }
                else
                {
                    throw new Exception("No parameters given to method GetTeam(int? teamNumber = null, string? teamName = null) in UserDAO.cs");
                }

                List<BsonDocument> documents = collection.Find(filter).ToList();
                List<ICollectionObject> users = new List<ICollectionObject>();

                foreach (BsonDocument document in documents)
                {
                    //Write adapter pattern
                    //users.Add(AdapterPatternUser(document))
                    User user;
                    BsonDocument documentOut;
                    try
                    {
                        user = new User(
                                            new BsonKeyValuePair("_id", document.GetValue("_id").ToString()),
                                            new BsonKeyValuePair("Username", document.GetValue("Username").ToString()),
                                            new BsonKeyValuePair("Password", document.GetValue("Password").ToString()),
                                            new BsonKeyValuePair("First Name", document.GetValue("First Name").ToString()),
                                            new BsonKeyValuePair("Last Name", document.GetValue("Last Name").ToString()),
                                            new BsonKeyValuePair("Role", document.GetValue("Role").ToString()),
                                            new BsonKeyValuePair("E-Mail", document.GetValue("E-Mail").ToString()),
                                            new BsonKeyValuePair("Phone Number", document.GetValue("Phone Number").ToString()),
                                            new BsonKeyValuePair("Location", document.GetValue("Location").ToString()),
                                            new BsonKeyValuePair("Teams", document.GetValue("Teams").ToString())
                                            );
                    }
                    catch (Exception ex)
                    {
                        //user mist een team
                        /*user = new User(
                                            new BsonKeyValuePair("_id", document.GetValue("_id").ToString()),
                                            new BsonKeyValuePair("Username", document.GetValue("Username").ToString()),
                                            new BsonKeyValuePair("Password", document.GetValue("Password").ToString()),
                                            new BsonKeyValuePair("First Name", document.GetValue("First Name").ToString()),
                                            new BsonKeyValuePair("Last Name", document.GetValue("Last Name").ToString()),
                                            new BsonKeyValuePair("Role", document.GetValue("Role").ToString()),
                                            new BsonKeyValuePair("E-Mail", document.GetValue("E-Mail").ToString()),
                                            new BsonKeyValuePair("Phone Number", document.GetValue("Phone Number").ToString()),
                                            new BsonKeyValuePair("Location", document.GetValue("Location").ToString())
                                            );*/
                    }
                    /*users.Add(user);*/

                }
                    return users;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AddUserTest()
        {
            string databaseName = "TicketSystemDB";
            string collectionName = "User";
            MongoClientInstance mongoClientInstance = MongoClientInstance.GetClientInstance();

            IMongoDatabase database = mongoClientInstance.Client.GetDatabase(databaseName);
            IMongoCollection<BsonDocument> collection = database.GetCollection<BsonDocument>(collectionName);


            BsonDocument document = new BsonDocument();
            document.Add("Team1Id", 1);
            document.Add("Team1Name", "Alpha Team");

            User user = new User(
                        new BsonKeyValuePair("_id", 2),
                        new BsonKeyValuePair("Username", "Reynard96Blazer"),
                        new BsonKeyValuePair("Password", "9669420"),
                        new BsonKeyValuePair("First Name", "Reynard"),
                        new BsonKeyValuePair("Last Name", "Blazer"),
                        new BsonKeyValuePair("Role", "Sudo"),
                        new BsonKeyValuePair("Email", "ReynardBlazer@Nanotrasen.net"),
                        new BsonKeyValuePair("PhoneNumber", 0644498323),
                        new BsonKeyValuePair("Location", "BayStation"),
                        new BsonKeyValuePair("Teams", document)
                        );

            BsonDocument userDocument = new BsonDocument();
            userDocument.Add(user.Id.Key, user.Id.Value);
            userDocument.Add(user.UserName.Key, user.UserName.Value);
            userDocument.Add(user.FirstName.Key, user.FirstName.Value);
            userDocument.Add(user.LastName.Key, user.LastName.Value);
            userDocument.Add(user.Role.Key, user.Role.Value);
            userDocument.Add(user.Email.Key, user.Email.Value);
            userDocument.Add(user.PhoneNumber.Key, user.PhoneNumber.Value);
            userDocument.Add(user.Location.Key, user.Location.Value);
            /*userDocument.Add(user.Teams.Key, user.Teams.Value);*/

            collection.InsertOne(userDocument);
        }
    }
}
