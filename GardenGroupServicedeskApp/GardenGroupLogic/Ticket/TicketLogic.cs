using GardenGroupDAL;
using GardenGroupModel;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;

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
        /// updates ticket database
        /// <item>made by floortje Tjeertes</item>
        /// </list>
        /// </summary>
        /// <param name="Tickettoupdate"></param>
        /// <param name="update"></param>
        public Ticket UpdateTicket(Ticket TickettoUpdate, Ticket update)
        {
            UserLogic userLogic = new UserLogic();

            BsonDocument document = TicketDAO.Update(TickettoUpdate, update);


            ///User user = userLogic.GetUser(document.GetElement("UserName").Value.ToString());


            /*Ticket localticket = new Ticket()
            {
                _id = document.GetElement("_id").Value.AsObjectId,
                Subject = document.GetElement("Subject").Value.ToString(),
                DateReported = DateTime.Parse(document.GetElement("DateReported").Value.ToString()),
                Incident = (IncidentType)Enum.Parse(typeof(IncidentType), document.GetElement("Incident").Value.ToString()),
                User = user,


                Impact = (Priority)Enum.Parse(typeof(Priority), document.GetElement("Impact").Value.ToString()),
                Urgency = (Priority)Enum.Parse(typeof(Priority), document.GetElement("Urgency").Value.ToString()),
                DeadLine = DateTime.Parse(document.GetElement("DeadLine").Value.ToString()),
                Status = (TicketStatus)Enum.Parse(typeof(TicketStatus), document.GetElement("Status").Value.ToString()),
                Description = document.GetElement("Description").Value.ToString()
            };
            return localticket;
            */
            return null;
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


                Ticket localticket = makeTicketWhitUser(document);
                tickets.Add(localticket);
            }
            return tickets;
        }

        /// <summary>
        /// deletes a ticket from the database
        /// <list type="bullet">
        /// <item>made by floortje Tjeertes</item>
        /// </list>
        /// </summary>
        /// <param name="ticket"></param>
        /// <returns></returns>
        public void DeleteTicket(Ticket ticket)
        {
            BsonDocument document = TicketDAO.Delete(ticket);



/*            return makeTicketWhitoutUser(document);
*/        }


        /// <summary>
        /// makes a ticket from a documenbt
        /// <list type="bullet">
        /// <item>made by floortje Tjeertes</item>
        /// </list>
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        private Ticket makeTicketWhitUser(BsonDocument document)
        {
          
                BsonArray user = document.GetElement("User").Value.AsBsonArray;
            

            Ticket localticket = new Ticket()
            {
                _id = document.GetElement("_id").Value.AsObjectId,
                Subject = document.GetElement("Subject").Value.ToString(),
                DateReported = DateTime.Parse(document.GetElement("DateReported").Value.ToString()),
                Incident = (IncidentType)Enum.Parse(typeof(IncidentType), document.GetElement("Incident").Value.ToString()),
                User = new User(
                    new BsonKeyValuePair("id", user[0].AsBsonDocument.GetElement("_id").Value),
                    new BsonKeyValuePair("userName", user[0].AsBsonDocument.GetElement("Username").Value.ToString()),                    
                    new BsonKeyValuePair("Salt", user[0].AsBsonDocument.GetElement("Salt").Value.ToString()),
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
            return localticket;
        }

        /// <summary>
        /// makes ticket from a document that has a fully filled in user
        /// <list type="bullet">
        /// <item>made by floortje Tjeertes</item>
        /// </list>
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>        

        public List<TicketDisplay> ListTicketsDisplay(List<Ticket> tickets)
        {
            UserLogic userLogic = new UserLogic();
            List<TicketDisplay> ticketsDisplay = new List<TicketDisplay>();
            for (int i = 0; i < tickets.Count; i++)
            {
                TicketDisplay ticketDisplay = new TicketDisplay()
                {
                    _id = tickets[i]._id,
                    DateReported = tickets[i].DateReported,
                    Subject = tickets[i].Subject,
                    Incident = tickets[i].Incident,
                    User = tickets[i].User,
                    Impact = tickets[i].Impact,
                    Urgency = tickets[i].Urgency,
                    DeadLine = tickets[i].DeadLine,
                    Status = tickets[i].Status,
                    Description = tickets[i].Description,
                    Priority = ticketDisplayPriority(tickets[i].Impact, tickets[i].Urgency),
                };

                ticketsDisplay.Add(ticketDisplay);
            }

            return ticketsDisplay;
        }

        public int ticketDisplayPriority(Priority Impact, Priority Urgency)
        {
            switch (Impact, Urgency)
            {
                case (Priority.high, Priority.high):
                    return 1;
                case (Priority.high, Priority.normal):
                    return 2;
                case (Priority.high, Priority.low):
                    return 3;
                case (Priority.normal, Priority.high):
                    return 2;
                case (Priority.normal, Priority.normal):
                    return 3;
                case (Priority.normal, Priority.low):
                    return 4;
                case (Priority.low, Priority.high):
                    return 3;
                case (Priority.low, Priority.normal):
                    return 4;
                case (Priority.low, Priority.low):
                    return 5;
                default:
                    return 5;
            }
        }
    }
}
