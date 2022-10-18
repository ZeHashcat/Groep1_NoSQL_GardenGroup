using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace GardenGroupLogic
{
    public class RandomGeneratedNumber
    {
        //create random based on keyLenght
        public byte[] GenerateRandomCryptoBytes(int keyLength)
        {
            RNGCryptoServiceProvider randomProvider = new RNGCryptoServiceProvider();
            byte[] random = new byte[keyLength];
            randomProvider.GetBytes(random);
            return random;
        }
    }
}
