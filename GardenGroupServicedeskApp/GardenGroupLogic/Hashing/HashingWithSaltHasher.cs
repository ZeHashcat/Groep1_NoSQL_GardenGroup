using GardenGroupModel;
using System.Security.Cryptography;
using System.Text;

namespace GardenGroupLogic
{
    public class HashingWithSaltHasher
    {
        //Use with AddUser
        public HashWithSaltResult NewHashWithSalt(string password, int saltLength, HashAlgorithm hashAlgorithm)
        {
            RandomGeneratedNumber randomNumberGenerator = new RandomGeneratedNumber();
            byte[] saltByte = randomNumberGenerator.GenerateRandomCryptoBytes(saltLength);
            byte[] passwordAsByte = Encoding.UTF8.GetBytes(password);
            List<byte> passwordWithSalt = new List<byte>();
            passwordWithSalt.AddRange(passwordAsByte);
            passwordWithSalt.AddRange(saltByte);
            byte[] hash = hashAlgorithm.ComputeHash(passwordWithSalt.ToArray());
            return new HashWithSaltResult(Convert.ToBase64String(saltByte), Convert.ToBase64String(hash));
        }
        //Use with Login        
        public HashWithSaltResult HashWithSalt(string password, HashWithSaltResult hashAndSalt, HashAlgorithm hashAlgorithm)
        {
            byte[] saltBytes = Convert.FromBase64String(hashAndSalt.Salt);
            byte[] passwordAsByte = Encoding.UTF8.GetBytes(password);
            List<byte> passwordWithSalt = new List<byte>();
            passwordWithSalt.AddRange(passwordAsByte);
            passwordWithSalt.AddRange(saltBytes);
            byte[] hash = hashAlgorithm.ComputeHash(passwordWithSalt.ToArray());
            return new HashWithSaltResult(Convert.ToBase64String(saltBytes), Convert.ToBase64String(hash));
        }
    }
}
