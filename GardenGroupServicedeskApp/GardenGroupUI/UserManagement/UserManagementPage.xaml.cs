using GardenGroupLogic;
using GardenGroupModel;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
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

namespace GardenGroupUI.UserManagement
{
    /// <summary>
    /// Interaction logic for UserManagementPage.xaml
    /// </summary>
    public partial class UserManagementPage : Page
    {
        UserLogic userLogic = new UserLogic();

        public UserManagementPage()
        {
            InitializeComponent();

            CreateClient(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);

            FillDataGrid();
        }

        private void CreateClient(string connectionString)
        {
            userLogic.CreateClient(connectionString);
        }

        private void FillDataGrid()
        {
            int id = 1;
            foreach (User user in userLogic.GetAllusers())
            {

                //ComboBoxUser.Items.Add(user.UserName.Value.ToString());              
                
                DataGridUserOverview.Items.Add(new { Id = id, Username = user.UserName.Value, FirstName = user.FirstName.Value, LastName = user.LastName.Value, Email = user.Email.Value});
                id++;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AppMenuWindow appWindow = new AppMenuWindow("../UserManagement/UserManagementPage.xaml");
            appWindow.Show();
        }
    }
}
