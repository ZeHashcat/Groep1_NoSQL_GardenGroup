using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GardenGroupModel
{
    internal class Ticket
    {
        DateTime dateReported;
        string subject;
        IncidentType incident;
        User user;
        Priority priority;
        DateTime deadline;

    }
}
