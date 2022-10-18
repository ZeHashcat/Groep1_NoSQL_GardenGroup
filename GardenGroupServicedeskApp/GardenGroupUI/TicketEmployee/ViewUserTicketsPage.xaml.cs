using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using GardenGroupModel;

namespace GardenGroupUI.TicketEmployee
{
    /// <summary>
    /// Interaction logic for ViewUserTicketsPage.xaml
    /// </summary>
    public partial class ViewUserTicketsPage : Page
    {
        RowDefinition rowDef;
        ColumnDefinition colDef;

        public ViewUserTicketsPage()
        {
            InitializeComponent();
        }

        public void TicketFetcher()
        {
            //fetch ticketarray from method in logic layer
            List<Ticket> tickets = new List<Ticket>();
            tickets.Add(new Ticket()
            {
                DateReported = DateTime.Today,
                Subject = "test1",
                Incident = IncidentType.service,
                User = new User(null, null, null, null, null, null, null, null, null, null),
                Impact = Priority.high,
                Urgency = Priority.low,
                DeadLine = DateTime.Now,
                Description = "this is a test."
            });
            tickets.Add(new Ticket()
            {
                DateReported = DateTime.Today,
                Subject = "test2",
                Incident = IncidentType.service,
                User = new User(null, null, null, null, null, null, null, null, null, null),
                Impact = Priority.high,
                Urgency = Priority.low,
                DeadLine = DateTime.Now,
                Description = "this is a test."
            });
            tickets.Add(new Ticket()
            {
                DateReported = DateTime.Today,
                Subject = "test3",
                Incident = IncidentType.service,
                User = new User(null, null, null, null, null, null, null, null, null, null),
                Impact = Priority.high,
                Urgency = Priority.low,
                DeadLine = DateTime.Now,
                Description = "this is a test."
            });

            PrintTickets(tickets);
        }

        public void PrintTickets(List<Ticket> tickets)
        {
            for (int i = 0; i < tickets.Count; i++)
            {
                rowDef = new RowDefinition();
                GridTicketOverview.RowDefinitions.Add(rowDef);
            }
        }
    }
}
