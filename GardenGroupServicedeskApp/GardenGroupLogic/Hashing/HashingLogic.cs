namespace GardenGroupLogic
{
    public class HashingLogic
    {

        /*The salt generation is as the example in the question.You can convert text to byte arrays using Encoding.UTF8.GetBytes(string). 
        If you must convert a hash to its string representation you can use Convert.ToBase64String and Convert.FromBase64String to convert it back.*/

        //Password must be hased
        //Save new password

        /*public static byte[] GetSaltHashedPassword(byte[] password, byte[] salt)
        {
            HashAlgorithm algorithm = new SHA256Managed();

            byte[] passwordWithSaltBytes =
              new byte[password.Length + salt.Length];

            for (int i = 0; i < password.Length; i++)
            {
                passwordWithSaltBytes[i] = password[i];
            }
            for (int i = 0; i < salt.Length; i++)
            {
                passwordWithSaltBytes[password.Length + i] = salt[i];
            }

            return algorithm.ComputeHash(passwordWithSaltBytes);            
        }*/


        //Zoekopdracht= c# hash with salt \/ \/
        /*public string CreateSalt(int size)
        {
            //Generate a cryptographic random number.
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] buff = new byte[size];
            rng.GetBytes(buff);
            return Convert.ToBase64String(buff);
        }*/

        /*static byte[] GenerateSaltedHash(byte[] plainText, byte[] salt)
        {
            HashAlgorithm algorithm = new SHA256Managed();

            byte[] plainTextWithSaltBytes =
              new byte[plainText.Length + salt.Length];

            for (int i = 0; i < plainText.Length; i++)
            {
                plainTextWithSaltBytes[i] = plainText[i];
            }
            for (int i = 0; i < salt.Length; i++)
            {
                plainTextWithSaltBytes[plainText.Length + i] = salt[i];
            }

            return algorithm.ComputeHash(plainTextWithSaltBytes);
        }*/
    }
}
