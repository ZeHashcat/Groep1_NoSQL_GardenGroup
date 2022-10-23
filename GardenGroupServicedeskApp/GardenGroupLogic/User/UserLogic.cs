using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using GardenGroupDAL;
using GardenGroupModel;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Driver;

namespace GardenGroupLogic
{
    public class UserLogic
    {
        private UserDAO userDAO;

        public UserLogic()
        {
            userDAO = new UserDAO();
        }

        public void CreateClient(string connectionString)
        {
            MongoClientInstance.GetClientInstance(connectionString);
        }

        public User GetUser(string username)
        {
            BsonDocument document = userDAO.GetUser(username);
            BsonKeyValuePair id = new BsonKeyValuePair("_id", document["_id"].ToString());
            BsonKeyValuePair userName = new BsonKeyValuePair("Username", document["Username"].ToString());
            BsonKeyValuePair salt = new BsonKeyValuePair("Salt", document["Salt"].ToString());
            BsonKeyValuePair password = new BsonKeyValuePair("Password", document["Password"].ToString());
            BsonKeyValuePair firstName = new BsonKeyValuePair("First Name", document["First Name"].ToString());
            BsonKeyValuePair lastName = new BsonKeyValuePair("Last Name", document["Last Name"].ToString());
            BsonKeyValuePair role = new BsonKeyValuePair("Role", document["Role"].ToString());
            BsonKeyValuePair email = new BsonKeyValuePair("E-Mail", document["E-Mail"].ToString());
            BsonKeyValuePair phoneNumber = new BsonKeyValuePair("Phone Number", document["Phone Number"].ToString());
            BsonKeyValuePair location = new BsonKeyValuePair("Location", document["Location"].ToString());
            BsonKeyValuePair? teams = null;

            try
            {
                teams = new BsonKeyValuePair("Teams", document["Teams"].ToString());
            }
            catch
            { }

            User user = new User(id, userName, password, salt, firstName, lastName, role, email, phoneNumber, location, teams);

            return user;
        }

        public bool CheckLogin(string username, string password)
        {
            HashingWithSaltHasher passwordHasher = new HashingWithSaltHasher();           

            //Getting hash and salt from database
            HashWithSaltResult hashAndSalt = userDAO.GetPassword(username);
            
            HashWithSaltResult hashWithSaltResult = passwordHasher.HashWithSalt(password, hashAndSalt, SHA512.Create());

            if (hashAndSalt.Hash == hashWithSaltResult.Hash)
                return true;
            
            else
                return false;
            
        }
        //validate email before changing the password
        public bool CheckEmail(string email)
        {

            string emailDB = userDAO.ValidateEmail(email);

            if (email == emailDB)
                return true;

            else
                return false;
        }
        public bool ChangePassword(string email, HashWithSaltResult hashWithSalt)
        {

            //If password can be changed (email exists)
            if (CheckEmail(email))
            {
                userDAO.ChangePassword(email, hashWithSalt);

                return true;
            }
            //Cant update password. Something went wrong
            else
                return false;
        }
        public bool AddUser(string username, HashWithSaltResult hashWithSalt, string firstname, string lastname, string email, double phonenumber, string role, string location)
        {        
            //User will be checked for duplicate values
            if (userDAO.CheckUserData(username, email, phonenumber))
            {
                //User will get added here
                userDAO.AddUser(username, hashWithSalt, firstname, lastname, email, phonenumber, role, location);
                return true;
            }
            //User has entered duplicate values. The user will be asked to enter diferent values
            else
                return false;
        }        
    }
}
