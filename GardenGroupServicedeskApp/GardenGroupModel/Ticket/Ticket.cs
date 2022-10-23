using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace GardenGroupModel
{
    public class Ticket
    {
        public ObjectId _id { get; set; }

        [BsonRepresentation(BsonType.DateTime)]
        public DateTime DateReported { get; set; }

        public string Subject { get; set; }

        [BsonRepresentation(BsonType.String)]
        public IncidentType Incident { get; set; }

        public User User { get; set; }

        [BsonRepresentation(BsonType.String)]
        public Priority Impact { get; set; }

        [BsonRepresentation(BsonType.String)]
        public Priority Urgency { get; set; }

        [BsonRepresentation(BsonType.DateTime)]
        public DateTime DeadLine { get; set; }

        [BsonRepresentation(BsonType.String)]
        public TicketStatus Status { get; set; }

        public string Description { get; set; }
    }
}