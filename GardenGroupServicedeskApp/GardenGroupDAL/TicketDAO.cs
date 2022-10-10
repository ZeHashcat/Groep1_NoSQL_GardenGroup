using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GardenGroupDAL
{
    public class TicketDAO
    {
        static string connstring = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
       public TicketDAO()
        {
            Console.WriteLine(connstring);

            MongoClientInstance MongoClientInstance = MongoClientInstance.GetClientInstance("mongodb+srv://Group1Admin:WNtAWH3BCCnMfTWi@ticketsystemcluster.0fedf6a.mongodb.net/?retryWrites=true");
         IAsyncCursor<string> list= MongoClientInstance.client.ListDatabaseNames();
            Console.WriteLine(list);
        }
    }
}
