using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GardenGroupDAL;
using GardenGroupModel;
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

        public bool CheckLogin(string username, string password, MongoClient client)
        {
            string passwordDB = userDAO.GetPassword(username, client);
            if (passwordDB == password)
                return true;

            else
                return false;
        }
    }
}
