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
    public class LocationService
    {
        private LocationDAO locationDAO;
        public LocationService()
        {
            locationDAO = new LocationDAO();
        }

        public async Task<List<string>> GetLocations()
        {
            List<string> locations = await Task.Run(() => locationDAO.GetLocations());
            return locations;

        }
    }
}
