using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GardenGroupModel
{
    public class UserTicketList : IWidget
    {
        private List<Ticket> ticketList;
        private string username;

        public UserTicketList(List<Ticket> ticketList, string username)
        {
            this.ticketList = ticketList;
            this.username = username;
        }

        public List<Ticket> TicketList { get { return ticketList; } }
        public string Username { get { return username; } }
    }
}
