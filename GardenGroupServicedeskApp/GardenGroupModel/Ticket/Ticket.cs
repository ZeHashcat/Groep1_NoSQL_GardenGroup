

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
       public TicketStatus Status;
       public String Description;
    }
}
