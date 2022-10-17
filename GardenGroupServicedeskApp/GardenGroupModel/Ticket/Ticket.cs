

using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace GardenGroupModel
{
    public class Ticket
    {
        [BsonRepresentation(BsonType.DateTime)]
        public DateTime DateReported;
        
        public string Subject;
        
        [BsonRepresentation(BsonType.String)]        
        public IncidentType Incident;
        
        public User User;
        
        [BsonRepresentation(BsonType.String)]         
        public Priority Impact;
        
        [BsonRepresentation(BsonType.String)]       
        public Priority Urgency;

        [BsonRepresentation(BsonType.DateTime)]
        public DateTime DeadLine;

        [BsonRepresentation(BsonType.String)]         
        public TicketStatus Status;

        public String Description;
    }
}
