using Ticket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GardenGroupModel
{
    internal class Ticket
    {
        DateTime DateReported;
        string Subject;
        IncidentType Incident;
        User user;
        Priority Impact;
        Priority Urgency;
        DateTime Deadline;
        TicketStatus status;
    }
}
