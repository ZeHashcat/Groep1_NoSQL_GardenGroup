using System;
using System.Collections.Generic;
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

            HashWithSaltResult hashAndSalt = userDAO.GetPassword(username);
            byte[] saltBytes = Encoding.ASCII.GetBytes(hashAndSalt.Salt);

            HashWithSaltResult hashWithSaltResult512 = passwordHasher.HashWithSalt(password, saltBytes, SHA512.Create());
            if (hashAndSalt.Hash == hashWithSaltResult512.Hash)
                return true;
            
            else
                return false;            
        }

        public bool CheckEmail(string email)
        {

            string emailDB = userDAO.ValidateEmail(email);

            if (email == emailDB)
                return true;

            else
                return false;
        }
    }
}
