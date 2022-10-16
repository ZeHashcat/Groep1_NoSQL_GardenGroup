using GardenGroupDAL;
using GardenGroupModel;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using System.Text.Json.Nodes;

namespace GardenGroupLogic
{
    public class TicketLogic
    {
        TicketDAO TicketDAO = new TicketDAO();

        /// <summary>
        /// <list type="bullet">
        /// <item>made by floortje Tjeertes</item>
        /// </list>
        /// </summary>
        /// <param name="ticket"></param>
        public void CreateTicket(Ticket ticket)
        {
            TicketDAO.Create(ticket);
        }

        /// <summary>
        /// <list type="bullet">
        /// <item>made by floortje Tjeertes</item>
        /// </list>
        /// </summary>
        /// <param name="Tickettoupdate"></param>
        /// <param name="update"></param>
        public void UpdateTicket(Ticket TickettoUpdate, Ticket update)
        {
            TicketDAO.Update(TickettoUpdate, update);
        }

        /// <summary>
        /// <list type="bullet">
        /// <item>made by floortje Tjeertes</item>
        /// </list>
        /// </summary>
        /// <returns></returns>
        public List<Ticket> ReadTicket()
        {

            return ListTickets(TicketDAO.Read()); ;
        }

        /// <summary>
        /// gets list of tickets 
        /// <list type="bullet">
        /// <item>made by floortje Tjeertes</item>
        /// </list>
        /// </summary>
        /// <param name="ticket"></param>
        /// <returns></returns>
        public List<Ticket> ReadTicket(Ticket ticket)
        {

            return ListTickets(TicketDAO.Read(ticket)); ;
        }

        /// <summary>
        /// <list type="bullet">
        /// <item>made by floortje Tjeertes</item>
        /// </list>
        /// </summary>
        /// <param name="ticketsBsonFormat"></param>
        /// <returns></returns>
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
