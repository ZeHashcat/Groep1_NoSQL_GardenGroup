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
        }

        // \/\/Waarschijnlijk niet nodig\/\/ \\
        private HashWithSaltResult ReadPassword(BsonDocument document)
        {
            //filter document to only get password

            string hashedPassword = document.GetValue("Password").ToString();
            string salt = document.GetValue("Salt").ToString();
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
                //After checking if the values dont match the existing ones in the system we will proceed and add the user
                else
                {
                    return true;
                }
            }
            return false;
        }

        public List<BsonDocument> GetUserList()
        {
            MongoClientInstance mongoClientInstance = MongoClientInstance.GetClientInstance();
            database = mongoClientInstance.Client.GetDatabase(databaseName);
            collection = database.GetCollection<BsonDocument>(collectionName);
            List<BsonDocument> users = collection.Find(new BsonDocument()).ToList();

            return users;
        }


        public void AddUser(string username, HashWithSaltResult hashWithSalt, string firstname, string lastname, string email, double phonenumber, string role, string location)
        {
            MongoClientInstance mongoClientInstance = MongoClientInstance.GetClientInstance();
            database = mongoClientInstance.Client.GetDatabase(databaseName);
            collection = database.GetCollection<BsonDocument>(collectionName);

            BsonDocument doc = new BsonDocument
            {                
                //Adding the data into a document                        
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

        /*public List<ICollectionObject> GetTeam(int? teamNumber = null, string? teamName = null)
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
                        
                    }
                    

                }
                return users;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }*/    
    }
}
