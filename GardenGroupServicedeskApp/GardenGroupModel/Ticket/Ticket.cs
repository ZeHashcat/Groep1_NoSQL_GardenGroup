using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GardenGroupModel
{
    public class Ticket
    {
        public DateTime DateReported;
        public string Subject;
        public IncidentType Incident;
        public User User;
        public Priority Impact;
        public Priority Urgency;
        public DateTime Deadline;
        public string Description;
    }
}
