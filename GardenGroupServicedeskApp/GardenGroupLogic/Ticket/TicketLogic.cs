using GardenGroupDAL;
using GardenGroupModel;
using MongoDB.Bson;

namespace GardenGroupLogic
{
    public class TicketLogic
    {
        TicketDAO TicketDAO = new TicketDAO();

        public void CreateTicket()
        {
        }
        public void UpdateTicker()
        {

        }
        public List<Ticket> ReadTicket()
        {
            List<BsonDocument> ticketsBsonFormat = TicketDAO.Read();
            List<Ticket> tickets = new List<Ticket>();


            foreach (BsonDocument document in ticketsBsonFormat)
            {
                Ticket ticket = new Ticket()
                {
                    Subject = document.GetElement("Subject").Value.ToString(),
                    Incident = (IncidentType)Enum.Parse( typeof(IncidentType),document.GetElement("Incident").Value.ToString()),
                    //User = new User()
                  
                    Impact = (Priority)Enum.Parse(typeof(Priority), document.GetElement("impact").Value.ToString()),
                    Urgency = (Priority)Enum.Parse(typeof(Priority), document.GetElement("urgency").Value.ToString()),
                    Deadline = DateTime.Parse(document.GetElement("DeadLine").Value.ToString()),
                    status = (TicketStatus)Enum.Parse(typeof(TicketStatus), document.GetElement("Status").Value.ToString()),
                    Description = document.GetElement("Description").Value.ToString()

                };
                tickets.Add(ticket);
            }




            return tickets;
        }
        public void DeleteTicket()
        {

        }
    }
}
