using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GardenGroupModel
{
    public class User
    {
        BsonKeyValuePair documentId;
        BsonKeyValuePair userId;
        BsonKeyValuePair firstName;
        BsonKeyValuePair lastName;
        BsonKeyValuePair password;
        BsonKeyValuePair role;
        BsonKeyValuePair? teams;

        public User(BsonKeyValuePair documentId, BsonKeyValuePair userId, BsonKeyValuePair firstName, BsonKeyValuePair lastName, BsonKeyValuePair password, BsonKeyValuePair role, BsonKeyValuePair? teams)
        {
            this.documentId = documentId;
            this.userId = userId;
            this.firstName = firstName;
            this.lastName = lastName;
            this.password = password;
            this.role = role;
            this.teams = teams;
        }
    }
}
