using MongoDB.Bson;

namespace GardenGroupModel
{
    public class BsonKeyValuePair
    {
        private string key;
        private BsonValue value;

        public BsonKeyValuePair(string key, BsonValue value)
        {
            this.key = key;
            this.value = value;
        }

        public string Key { get { return key; } }
        public BsonValue Value { get { return value; } }
    }
}
