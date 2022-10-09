using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GardenGroupModel;
using GardenGroupDAL;

namespace GardenGroupLogic
{
    public class TicketLogic
    {
        //Client client = new Client();
        private TicketDAO ticketDAO;

        public TicketLogic()
        {
            this.ticketDAO = new TicketDAO();
        }

        public void CreateTicket()
        {
        }
        public void UpdateTicker()
        {

        }
        public void ReadTicket()
        {

        }
        public void DeleteTicket()
        {

        }
        public UserTicketList FillTicketList(string username)
        {
            List<Ticket> tickets = ticketDAO.FillTicketList(username);
            UserTicketList userTicketList = new UserTicketList(tickets, username);
            return userTicketList;
        }
    }
}
