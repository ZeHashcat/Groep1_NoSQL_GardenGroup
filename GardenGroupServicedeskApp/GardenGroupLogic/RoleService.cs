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
    public class RoleService
    {
        private RoleDAO roleDAO;
        public RoleService()
        {
            roleDAO = new RoleDAO();
        }

        public async Task<List<string>> GetRoles()
        {
            List<string> roles = await Task.Run(() => roleDAO.GetRoles());
            return roles;

        }
    }
}
