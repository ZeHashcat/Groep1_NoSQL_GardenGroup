using System;
using System.Collections.Generic;
using System.Linq;
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
using GardenGroupLogic;
using GardenGroupModel;

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
            List<ICollectionObject> tickets = ticketLogic.FillTicketList("ZeHashcat");
            string[,] columnHeaders = SetColumnHeaders();
            listViewWidget.Items.Clear();
            listViewWidget = ListWidget.GetListView(listViewWidget, tickets, columnHeaders);
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
