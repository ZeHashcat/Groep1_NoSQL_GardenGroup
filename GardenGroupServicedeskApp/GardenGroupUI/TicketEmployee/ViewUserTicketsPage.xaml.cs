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
        List<Ticket> ticketsOnStatus;

        public ViewUserTicketsPage()
        {
            InitializeComponent();

            PageLoader();
        }

        public void PageLoader()
        {
            FillTicketsList();

            FillComboBox();

            TicketLoader();
        }

        public void FillTicketsList()
        {
            //get logged in user
            userLoggedIn = UserInstance.GetUserInstance().User;

            //fill ticketlist from method in logic layer
            tickets = ticketLogic.ReadTicket();

            //filter list for viewing
            if (userLoggedIn.Role.Value.ToString() == "User")
            {
                tickets = FilterTickets();
            }
        }

        public void FillComboBox()
        {
            //add items to combobox
            ComboBoxStatus.Items.Add("open");
            ComboBoxStatus.Items.Add("closed");
            ComboBoxStatus.Items.Add("resolved");

            //select standard box view
            ComboBoxStatus.SelectedItem = "open";
            ticketsOnStatus = FilterOnStatus("open");

            ComboBoxStatus.SelectionChanged += ComboBoxClick;
        }

        private void ComboBoxClick(object sender, SelectionChangedEventArgs e)
        {
            ticketsOnStatus = FilterOnStatus(ComboBoxStatus.SelectedItem.ToString());

            TicketLoader();
        }

        public List<Ticket> FilterOnStatus(string status)
        {
            //
            List<Ticket> filteredTickets = new List<Ticket>();

            //
            for (int i = 0; i < tickets.Count; i++)
            {
                if (tickets[i].Status.ToString() == status)
                {
                    filteredTickets.Add(tickets[i]);
                }
            }
            return filteredTickets;
        }

        public void TicketLoader()
        {
            FillTicketsList();

            //make and fill filtered ticketdisplaylist modified for displaying (contains priority)
            List<TicketDisplay> ticketsDisplay = ticketLogic.ListTicketsDisplay(ticketsOnStatus);

            //fill datagrid with displaytickets
            DataGridTicketOverview.ItemsSource = ticketsDisplay;
        }

        public List<Ticket> FilterTickets()
        {
            //make list of filtered tickets
            List<Ticket> filteredTickets = new List<Ticket>();

            //fill list
            for (int i = 0; i < tickets.Count; i++)
            {
                if (tickets[i].User.UserName.Value.ToString() == userLoggedIn.UserName.Value.ToString() && tickets[i].Status.ToString() == "open")
                {
                    filteredTickets.Add(tickets[i]);
                }
            }
            return filteredTickets;
        }

        private void ButtonRefresh_Click(object sender, RoutedEventArgs e)
        {            
            RefreshDataGrid();
            RefreshDataGrid();
        }

        public void RefreshDataGrid()
        {
            ticketsOnStatus = FilterOnStatus(ComboBoxStatus.SelectedItem.ToString());

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
