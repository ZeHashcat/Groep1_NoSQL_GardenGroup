using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GardenGroupDAL;
using GardenGroupModel;

namespace GardenGroupLogic
{
    public class LoginService
    {
        private LoginDAO loginDAO;
        public LoginService()
        {
            loginDAO = new LoginDAO();
        }

    }
}
