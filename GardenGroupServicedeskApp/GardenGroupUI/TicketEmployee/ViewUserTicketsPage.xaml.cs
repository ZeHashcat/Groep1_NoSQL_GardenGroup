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
        User userLoggedIn;
        List<Ticket> tickets;

        public ViewUserTicketsPage()
        {
            InitializeComponent();

            PageLoader();
        }

        public void PageLoader()
        {
            //get logged in user
            userLoggedIn = UserInstance.GetUserInstance().User;

            TicketLoader();
        }

        public void TicketLoader()
        {
            //fill ticketlist from method in logic layer
            tickets = ticketLogic.ReadTicket();

            //filter list for viewing
            if (userLoggedIn.Role.Value.ToString() == "User")
            {
                //hide create button from regular employee
                ButtonCreate.Visibility = Visibility.Hidden;

                tickets = FilterTicketsList();
            }

            //make and fill filtered ticketdisplaylist modified for displaying (contains priority)
            List<TicketDisplay> ticketsDisplay = ticketLogic.ListTicketsDisplay(tickets);

            //fill datagrid with displaytickets
            DataGridTicketOverview.ItemsSource = ticketsDisplay;
        }

        public List<Ticket> FilterTicketsList()
        {
            //make list of filtered tickets
            List<Ticket> filteredTickets = new List<Ticket>();

            //fill list
            for (int i = 0; i < tickets.Count; i++)
            {
                if (tickets[i].User.UserName.Value.ToString() == userLoggedIn.UserName.Value.ToString())
                {
                    filteredTickets.Add(tickets[i]);
                }
            }
            return filteredTickets;
        }

        private void ButtonRefresh_Click(object sender, RoutedEventArgs e)
        {
            TicketLoader();
        }

        private void DataGridRow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            CreateCRUDPage(SelectCRUDState());
        }

        public CRUDState SelectCRUDState()
        {
            switch (userLoggedIn.Role.Value.ToString())
            {
                case "User":
                    return CRUDState.Read;
                case "Admin":
                    return CRUDState.Update;
                default:
                    return CRUDState.Read;
            }
        }

        private void ButtonCreate_Click(object sender, RoutedEventArgs e)
        {
            CreateCRUDPage(CRUDState.Create);
        }

        public void CreateCRUDPage(CRUDState state)
        {
            Ticket ticket = null;
            if (state != CRUDState.Create)
            {
                ticket = tickets[DataGridTicketOverview.SelectedIndex];
            }
            //make window for c.r.u.d.
            TicketWindow ticketWindow = new TicketWindow(state, this, ticket);
            //set ticketwindow owner and show it
            ticketWindow.Owner = AppMenuWindow.GetWindow(this);
            ticketWindow.Activate();
            ticketWindow.Show();
            //disable this window
            AppMenuWindow.GetWindow(this).IsEnabled = false;
        }
    }
}
