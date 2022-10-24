using GardenGroupLogic;
using GardenGroupModel;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace GardenGroupUI.TicketEmployee
{
    /// <summary>
    /// Interaction logic for ViewUserTicketsPage.xaml
    /// </summary>
    public partial class ViewUserTicketsPage : Page
    {
        TicketLogic ticketLogic = new TicketLogic();
        List<Ticket> tickets;
        List<TicketDisplay> ticketsDisplay;
        List<User> users;
        User userLoggedIn;

        public ViewUserTicketsPage()
        {
            InitializeComponent();

            TicketLoader();


        }

        public void TicketLoader()
        {
            //fill ticketlist from method in logic layer
            tickets = ticketLogic.ReadTicket();

            //make and fill ticketdisplaylist modified for displaying (contains priority)
            ticketsDisplay = new List<TicketDisplay>();
            ticketsDisplay = ticketLogic.ListTicketsDisplay(tickets);

            //filter list for viewing
            /*if (userLoggedIn.Role.ToString() == "RegularEmployee")
            {
                FilterTicketsList();
            }*/

            //fill datagrid with displaytickets
            DataGridTicketOverview.ItemsSource = ticketsDisplay;
        }

        public void FilterTicketsList()
        {
            for (int i = 0; i < ticketsDisplay.Count; i++)
            {
                if (ticketsDisplay[i].User.UserName.Value.ToString() != userLoggedIn.UserName.Value.ToString())
                {
                    ticketsDisplay.Remove(ticketsDisplay[i]);
                }
            }
        }

        private void ButtonRefresh_Click(object sender, RoutedEventArgs e)
        {
            TicketLoader();
        }

        private void DataGridRow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //make window for c.r.u.d.
            TicketWindow ticketWindow = new TicketWindow(CRUDState.Create, tickets[DataGridTicketOverview.SelectedIndex], this);
            //set ticketwindow owner and show it
            ticketWindow.Owner = AppMenuWindow.GetWindow(this);
            ticketWindow.Activate();
            ticketWindow.Show();
            //disable this window
            AppMenuWindow.GetWindow(this).IsEnabled = false;
        }
    }
}
