using GardenGroupLogic;
using System.Windows;
using System.Windows.Controls;

namespace GardenGroupUI
{
    /// <summary>
    /// Interaction logic for UserTicketsList.xaml
    /// </summary>
    public partial class UserTicketsList : Page
    {
        public UserTicketsList()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            TicketLogic ticketLogic = new TicketLogic();
            /*List<ICollectionObject> tickets = ticketLogic.FillTicketList("ZeHashcat");*/
            string[,] columnHeaders = SetColumnHeaders();
            listViewWidget.Items.Clear();
            /*listViewWidget = ListWidget.GetListView(listViewWidget, tickets, columnHeaders);*/
        }

        private static string[,] SetColumnHeaders()
        {
            string[,] columnHeaders = new string[4, 2];
            columnHeaders[0, 0] = "Id";
            columnHeaders[0, 1] = "Id";
            columnHeaders[1, 0] = "Status";
            columnHeaders[1, 1] = "Status";
            columnHeaders[2, 0] = "Ticket Author";
            columnHeaders[2, 1] = "TicketAuthor";
            columnHeaders[3, 0] = "Time since Creation";
            columnHeaders[3, 1] = "DateTimeCreated";
            return columnHeaders;
        }
    }
}
