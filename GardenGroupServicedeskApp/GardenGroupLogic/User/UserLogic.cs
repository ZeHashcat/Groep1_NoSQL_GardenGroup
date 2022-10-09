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
            BsonKeyValuePair userName = new BsonKeyValuePair("UserName", document["UserName"].ToString());
            BsonKeyValuePair password = new BsonKeyValuePair("Password", document["Password"].ToString());
            BsonKeyValuePair firstName = new BsonKeyValuePair("First Name", document["First Name"].ToString());
            BsonKeyValuePair lastName = new BsonKeyValuePair("Last Name", document["Last Name"].ToString());
            BsonKeyValuePair role = new BsonKeyValuePair("Role", document["Role"].ToString());
            BsonKeyValuePair email = new BsonKeyValuePair("E-Mail", document["E-Mail"].ToString());
            BsonKeyValuePair phoneNumber = new BsonKeyValuePair("Phone Number", document["Phone Number"].ToString());
            BsonKeyValuePair location = new BsonKeyValuePair("Location", document["Location"].ToString());
            BsonKeyValuePair? teams = null;

            if (document["Teams"].ToString() != null)
            {
                teams = new BsonKeyValuePair("Teams", document["Teams"].ToString());
            }

            User user = new User(id, userName, password, firstName, lastName, role, email, phoneNumber, location, teams);


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

            //"ZtzS7zL/RahJMy+4BtrntmymitRa5JqASTUxCzlgQ/HV9azfadu7gEKEaPdaG6Wz0WcYS+RN0pVnJnscgjq4RA=="
            //"o+OZF3wHMEAyaa6T5qvJOqEvP6O1ydpMxih+GsEruegWF7ajtK1ntZAstGOg61klWoPu5/ZCII+hMdUPYJjPhQ=="


            //"BSA8iVwUwV0Dci3EeLa9e8lmu2OmQ6y6JijYUYN6PjSFypLxw5YCFbPdcTFtimKw5EmgTsRg5Gu0Cec4Mw7y0A=="
            //"BSA8iVwUwV0Dci3EeLa9e8lmu2OmQ6y6JijYUYN6PjSFypLxw5YCFbPdcTFtimKw5EmgTsRg5Gu0Cec4Mw7y0A=="

            /*PasswordWithSaltHasher pwHasher = new PasswordWithSaltHasher();
            
            HashWithSaltResult hashResultSha512 = pwHasher.HashWithSalt("ultra_safe_P455w0rD", 64, SHA512.Create());*/
        }

       /* public bool CheckEMAil()
        {


            if ()
                return true;

            else
                return false;
        }*/
    }
}
