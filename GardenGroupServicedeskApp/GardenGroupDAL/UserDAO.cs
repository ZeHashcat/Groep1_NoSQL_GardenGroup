﻿using System;
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

        private string databaseName = "TicketSystemDB";
        private string collectionName = "User";

        public string GetPassword(string username)
        {
            try
            {
                MongoClientInstance mongoClientInstance = MongoClientInstance.GetClientInstance();
                database = mongoClientInstance.Client.GetDatabase(databaseName);
                collection = database.GetCollection<BsonDocument>(collectionName);
                
                FilterDefinition<BsonDocument> filter = Builders<BsonDocument>.Filter.Eq("UserName", username);
                BsonDocument document = collection.Find(filter).First();
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
        private string ReadPassword(BsonDocument document)
        {
            //filter document to only get password
            try
            {
                string password = document.GetValue("Password").ToString();

                return password;
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
                MongoClientInstance mongoClientInstance = MongoClientInstance.GetClientInstance();
                database = mongoClientInstance.Client.GetDatabase(databaseName);
                collection = database.GetCollection<BsonDocument>(collectionName);

                FilterDefinition<BsonDocument> filter = Builders<BsonDocument>.Filter.Eq("UserName", username);
                BsonDocument document = collection.Find(filter).First();
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
                    return document;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Data could not be retrieved from the database. Please try again, error: " + ex.Message);
            }
        }
    }
}
