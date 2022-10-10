using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace GardenGroupDAL
{
    //Handy to know:
    //https://stackoverflow.com/questions/1086521/what-is-the-difference-between-dao-and-dal
    public class MongoClientInstance
    {
        private static MongoClientInstance? instance = null;
        private static readonly object padlock = new object();
        public MongoClient client;


        private MongoClientInstance(string connectionString)
        {
            client = SetupClient(connectionString);
        }

        public MongoClient SetupClient(string connectionString)
        {
            MongoClientSettings settings = MongoClientSettings.FromConnectionString(connectionString);
            settings.ServerApi = new ServerApi(ServerApiVersion.V1);
            client = new MongoClient(settings);
            return client;
        }

        public MongoClient Client { get { return client; }  }

        //Singleton with parameters
        public static MongoClientInstance GetClientInstance(string? connectionString = null)
        {
            try
            {
                if (instance == null)
                {
                    lock (padlock)
                    {
                        if (instance == null) //EXPLANATION: If first if statement goes off in first instance, at same time in second instance, instance gets created like below,                  
                        {                    //EXPLANATION: then first instance padlocks with intention of creating instance, but now has to do second check preventing another instance from being created.
                            instance = new MongoClientInstance(connectionString);
                        }  
                    }
                }
                return instance;
            }catch (Exception ex)
            {
                throw new Exception($"Something went wrong, was it the connectionstring?:\n {ex}");//NOTE: Make this different? <<---`-`/\/\`-`(REMINDER)
            }  
        }
    }
}
