using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace GardenGroupModel
{
    public class TemplateTestDocument
    {
        private BsonDocument document;

        public TemplateTestDocument(string name, int id)
        {
            this.document = CreateTestDocument(name, id);
        }

        public BsonDocument Document { get { return document; } }

        private BsonDocument CreateTestDocument(string name, int id)
        {
            BsonDocument document = new BsonDocument
            {
                { "name", name },
                { "id", id }
            };
            return document;
        }
    }
}
