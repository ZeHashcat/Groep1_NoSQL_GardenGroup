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
using GardenGroupModel;
using GardenGroupLogic;

namespace GardenGroupUI
{
    /// <summary>
    /// Interaction logic for TeamUsersList.xaml
    /// </summary>
    public partial class TeamUsersList : Page
    {
        public TeamUsersList()
        {
            InitializeComponent();
            
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            UserLogic userLogic = new UserLogic();
            List<ICollectionObject> Users = userLogic.GetTeam(1);
            string[,] columnHeaders = SetColumnHeaders();
            listViewWidget.Items.Clear();
            listViewWidget = ListWidget.GetListView(listViewWidget, Users, columnHeaders);
        }

        private static string[,] SetColumnHeaders()
        {
            string[,] columnHeaders = new string[3, 2];
            columnHeaders[0, 0] = "First Name";
            columnHeaders[0, 1] = "FirstName.Value";
            columnHeaders[1, 0] = "Role";
            columnHeaders[1, 1] = "Role.Value";
            columnHeaders[2, 0] = "Amount of tickets";
            columnHeaders[2, 1] = "TicketAmount";
            return columnHeaders;
        }

        
    }
}
