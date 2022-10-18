﻿using System;
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
        private IMongoCollection<BsonDocument> collection = null;
        private IMongoDatabase database = null;
        

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
    }
}
