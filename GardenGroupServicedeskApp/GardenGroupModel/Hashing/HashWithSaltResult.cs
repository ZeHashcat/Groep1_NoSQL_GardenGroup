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
