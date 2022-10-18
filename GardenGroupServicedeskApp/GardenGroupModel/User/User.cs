using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GardenGroupModel
{
    public class User : ICollectionObject
    {
        private BsonKeyValuePair id;
        private BsonKeyValuePair userName;
        private BsonKeyValuePair password;
        private BsonKeyValuePair firstName;
        private BsonKeyValuePair lastName;
        private BsonKeyValuePair role;
        private BsonKeyValuePair email;
        private BsonKeyValuePair phoneNumber;
        private BsonKeyValuePair location;
        private BsonKeyValuePair? teams;

        public User(BsonKeyValuePair id, BsonKeyValuePair userName, BsonKeyValuePair password, BsonKeyValuePair firstName, BsonKeyValuePair lastName, BsonKeyValuePair role, BsonKeyValuePair email, BsonKeyValuePair phoneNumber, BsonKeyValuePair location, BsonKeyValuePair? teams = null)
        {
            this.id = id;
            this.userName = userName;
            this.password = password;
            this.firstName = firstName;
            this.lastName = lastName;
            this.role = role;
            this.email = email;
            this.phoneNumber = phoneNumber;
            this.location = location;
            this.teams = teams ?? null;
        }


        public BsonKeyValuePair Id { get { return this.id; } }
        public BsonKeyValuePair UserName { get { return this.userName; } }
        public BsonKeyValuePair Password { get { return this.password; } }
        public BsonKeyValuePair FirstName { get { return this.firstName; } }
        public BsonKeyValuePair LastName { get { return this.lastName; } }
        public BsonKeyValuePair Role { get { return this.role; } }
        public BsonKeyValuePair Email { get { return this.email; } }
        public BsonKeyValuePair PhoneNumber { get { return this.phoneNumber; } }   
        public BsonKeyValuePair Location { get { return this.location; } }
        public BsonKeyValuePair Teams { get { return this.teams; } }
    }
}
