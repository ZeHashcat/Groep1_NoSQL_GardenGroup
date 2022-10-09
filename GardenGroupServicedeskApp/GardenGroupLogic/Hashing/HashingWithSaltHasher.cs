using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using GardenGroupModel;

namespace GardenGroupLogic
{
    public class HashingWithSaltHasher
    {

        /*public HashWithSaltResult HashWithSalt(string password, int saltLength, HashAlgorithm hashAlgo)
        {
            RNG rng = new RNG();
            byte[] saltBytes = rng.GenerateRandomCryptographicBytes(saltLength);
            byte[] passwordAsBytes = Encoding.UTF8.GetBytes(password);
            List<byte> passwordWithSaltBytes = new List<byte>();
            passwordWithSaltBytes.AddRange(passwordAsBytes);
            passwordWithSaltBytes.AddRange(saltBytes);
            byte[] digestBytes = hashAlgo.ComputeHash(passwordWithSaltBytes.ToArray());
            return new HashWithSaltResult(Convert.ToBase64String(saltBytes), Convert.ToBase64String(digestBytes));
        }*/

        public HashWithSaltResult HashWithSalt(string password, int saltLength, HashAlgorithm hashAlgorithm)
        {
            RandomNumberGenerator randomNumberGenerator = new RandomNumberGenerator();
            byte[] saltByte = randomNumberGenerator.GenerateRandomCryptoBytes(saltLength);
            byte[] passwordAsByte = Encoding.UTF8.GetBytes(password);
            List<byte> passwordWithSalt = new List<byte>();
            passwordWithSalt.AddRange(passwordAsByte);
            passwordWithSalt.AddRange(saltByte);
            byte[] digest = hashAlgorithm.ComputeHash(passwordWithSalt.ToArray());
            return new HashWithSaltResult(Convert.ToBase64String(saltByte), Convert.ToBase64String(digest));
        }
    }
}
