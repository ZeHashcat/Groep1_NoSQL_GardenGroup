using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GardenGroupModel
{
    public class HashWithSaltResult
    {
        public string Salt { get; }
        public string Hash { get; set; }

        public HashWithSaltResult(string salt, string hash)
        {
            Salt = salt;
            Hash = hash;
        }
    }
}
