using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace GardenGroupModel
{
    //Is there not an inbuild function that does what ClientSetup does? <<---`-`/\/\`-`(REMINDER)
    public static class Client
    {
        public static MongoClient ClientSetup(string connectionString, string name)
        {
            MongoClientSettings settings = MongoClientSettings.FromConnectionString(connectionString);
            settings.ServerApi = new ServerApi(ServerApiVersion.V1);
            MongoClient client = new MongoClient(settings);
            return client;
        }
    }
}
