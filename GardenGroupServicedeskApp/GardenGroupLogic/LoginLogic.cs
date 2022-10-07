﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GardenGroupDAL;
using GardenGroupModel;
using MongoDB.Driver;

namespace GardenGroupLogic
{
    public class LoginLogic
    {
        private LoginDAO loginDAO;

        public LoginLogic()
        {
            loginDAO = new LoginDAO();

        }

        public bool CheckLogin(string username, string password, MongoClient client)
        {
            string passwordDB = loginDAO.GetPassword(username, client);
            if (passwordDB == password)
                return true;

            else
                return false;
        }
    }
}