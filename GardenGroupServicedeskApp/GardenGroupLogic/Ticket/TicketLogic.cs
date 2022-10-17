using GardenGroupDAL;
using GardenGroupModel;
using MongoDB.Bson;

namespace GardenGroupLogic
{
    public class TicketLogic
    {
        TicketDAO TicketDAO = new TicketDAO();

        public void CreateTicket(Ticket ticket)
        {
            TicketDAO.Create(ticket);
        }
        public void UpdateTicker()
        {

        }
        public List<Ticket> ReadTicket()
        {
            return ListTickets(TicketDAO.Read()); ;
        }
        public List<Ticket> ReadTicket(Ticket ticket)
        {
            return ListTickets(TicketDAO.Read(ticket)); ;
        }

        private List<Ticket> ListTickets(List<BsonDocument> ticketsBsonFormat)
        {
            List<Ticket> tickets = new List<Ticket>();

            foreach (BsonDocument document in ticketsBsonFormat)
            {
                BsonArray user = document.GetElement("User").Value.AsBsonArray;

                Ticket localticket = new Ticket()
                {
                    Subject = document.GetElement("Subject").Value.ToString(),
                    Incident = (IncidentType)Enum.Parse(typeof(IncidentType), document.GetElement("Incident").Value.ToString()),
                    User = new User(
                        new BsonKeyValuePair("id", user[0].AsBsonDocument.GetElement("_id").Value.ToInt32()),
                        new BsonKeyValuePair("userName", user[0].AsBsonDocument.GetElement("Username").Value.ToString()),
                        new BsonKeyValuePair("password", user[0].AsBsonDocument.GetElement("Password").Value.ToString()),
                        new BsonKeyValuePair("firstName", user[0].AsBsonDocument.GetElement("First Name").Value.ToString()),
                        new BsonKeyValuePair("lastName", user[0].AsBsonDocument.GetElement("Last Name").Value.ToString()),
                        new BsonKeyValuePair("role", user[0].AsBsonDocument.GetElement("Role").Value.ToString()),
                        new BsonKeyValuePair("email", user[0].AsBsonDocument.GetElement("E-Mail").Value.ToString()),
                        new BsonKeyValuePair("phoneNumber", user[0].AsBsonDocument.GetElement("Phone Number").Value.ToString()),
                        new BsonKeyValuePair("location", user[0].AsBsonDocument.GetElement("Location").Value.ToString())

                        ),


                    Impact = (Priority)Enum.Parse(typeof(Priority), document.GetElement("Impact").Value.ToString()),
                    Urgency = (Priority)Enum.Parse(typeof(Priority), document.GetElement("Urgency").Value.ToString()),
                    DeadLine = DateTime.Parse(document.GetElement("DeadLine").Value.ToString()),
                    Status = (TicketStatus)Enum.Parse(typeof(TicketStatus), document.GetElement("Status").Value.ToString()),
                    Description = document.GetElement("Description").Value.ToString()

                };
                tickets.Add(localticket);
            }
            return tickets;
        }

        public void DeleteTicket()
        {

        }
    }
}
