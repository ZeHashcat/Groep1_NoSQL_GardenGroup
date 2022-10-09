using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GardenGroupModel
{
    public class Ticket : ICollectionObject
    {
        private int id;
        private string status;
        private string ticketAuthor;
        private string dateTimeCreated;

        public Ticket(int id, string status, string ticketAuthor, string dateTimeCreated)
        {
            this.id = id;
            this.status = status;
            this.ticketAuthor = ticketAuthor;
            this.dateTimeCreated = dateTimeCreated;
        }
        public int Id { get => id; }
        public string Status { get => status;  }
        public string TicketAuthor { get => ticketAuthor; }
        public string DateTimeCreated { get => dateTimeCreated; }


        DateTime DateReported;
        string Subject;
        IncidentType Incident;
        //User user;
        Priority Impact;
        Priority Urgency;
        DateTime Deadline;


    }
}
