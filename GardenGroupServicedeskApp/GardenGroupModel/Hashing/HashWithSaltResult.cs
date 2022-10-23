using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GardenGroupModel
{
    public class HashWithSaltResult
    {
        public byte[] Salt { get; }
        public byte[] Hash { get; set; }

        public HashWithSaltResult(byte[] salt, byte[] hash)
        {
            Salt = salt;
            Hash = hash;
        }
    }
}
