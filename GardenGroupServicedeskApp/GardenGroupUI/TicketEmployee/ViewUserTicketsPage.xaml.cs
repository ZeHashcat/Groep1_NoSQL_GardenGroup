using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
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
using GardenGroupLogic;
using GardenGroupModel;
using static System.Net.Mime.MediaTypeNames;

namespace GardenGroupUI.TicketEmployee
{
    /// <summary>
    /// Interaction logic for ViewUserTicketsPage.xaml
    /// </summary>
    public partial class ViewUserTicketsPage : Page
    {
        TicketLogic ticketLogic = new TicketLogic();
        List<Ticket> tickets;

        public ViewUserTicketsPage()
        {
            InitializeComponent();

            TicketLoader();
        }

        public void TicketLoader()
        {
            //fetch ticketarray from method in logic layer
            tickets = ticketLogic.ReadTicket();
            
            DataGridTicketOverview.ItemsSource = tickets;

            /*< TextBlock Grid.Row = "0" Grid.Column = "0" Text = "Deadline" HorizontalAlignment = "Left" Margin = "5,0,0,0" />
            < TextBlock Grid.Row = "0" Grid.Column = "1" Text = "Subject" HorizontalAlignment = "Left" Margin = "5,0,0,0" />
            < TextBlock Grid.Row = "0" Grid.Column = "2" Text = "User" HorizontalAlignment = "Left" Margin = "5,0,0,0" />
            < TextBlock Grid.Row = "0" Grid.Column = "3" Text = "Description" HorizontalAlignment = "Left" Grid.ColumnSpan = "2" Margin = "5,0,0,0" />
            < TextBlock Grid.Row = "0" Grid.Column = "4" Text = "Date" HorizontalAlignment = "Left" Margin = "5,0,0,0" />
            < TextBlock Grid.Row = "0" Grid.Column = "5" Text = "Status" HorizontalAlignment = "Left" Margin = "5,0,0,0" VerticalAlignment = "Top" />*/
        }

        private void ButtonRefresh_Click(object sender, RoutedEventArgs e)
        {
            TicketLoader();
        }

        private void DataGridRow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            TicketWindow ticketWindow = new TicketWindow(CRUDState.Read, tickets[0]);
            ticketWindow.Show();
            AppWindow.GetWindow(this).IsEnabled = false;
        }
    }
}
