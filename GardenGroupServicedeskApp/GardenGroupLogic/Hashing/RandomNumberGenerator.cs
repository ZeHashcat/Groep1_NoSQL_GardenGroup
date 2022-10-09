using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace GardenGroupLogic
{
    public class RandomNumberGenerator
    {



        /*public string GenerateRandomCryptographicKey(int keyLength)
        {
            return Convert.ToBase64String(GenerateRandomCryptographicBytes(keyLength));
        }

        public byte[] GenerateRandomCryptographicBytes(int keyLength)
        {
            RNGCryptoServiceProvider rngCryptoServiceProvider = new RNGCryptoServiceProvider();
            byte[] randomBytes = new byte[keyLength];
            rngCryptoServiceProvider.GetBytes(randomBytes);
            return randomBytes;
        }*/

        public string GenerateRandomCryptoKey(int keyLength)
        {
            return Convert.ToBase64String(GenerateRandomCryptoBytes(keyLength));
        }

        public byte[] GenerateRandomCryptoBytes(int keyLength)
        {
            RNGCryptoServiceProvider randomProvider = new RNGCryptoServiceProvider();
            byte[] random = new byte[keyLength];
            randomProvider.GetBytes(random);
            return random;
        }
    }
}
