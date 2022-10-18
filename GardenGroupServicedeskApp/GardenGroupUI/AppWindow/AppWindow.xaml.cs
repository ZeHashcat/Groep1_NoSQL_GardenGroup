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
using System.Windows.Shapes;
using GardenGroupModel;
using GardenGroupLogic;

namespace GardenGroupUI
{
    /// <summary>
    /// Interaction logic for AppWindow.xaml
    /// </summary>
    public partial class AppWindow : Window
    {

        public AppWindow()
        {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            //frameContent.Source = new Uri("../Login/LoginPage.xaml", UriKind.Relative);
            frameContent.Source = new Uri("AppMainEmployeePage.xaml", UriKind.Relative);
            //frameContent.Source = new Uri("AppMainServiceDeskEmployeePage.xaml", UriKind.Relative);
            //frameContent.Source = new Uri("../TicketServiceDeskEmployee/CRUDPage.xaml", UriKind.Relative);

        private void CreateClient(string connectionString)
        {
            UserLogic loginLogic = new UserLogic();
            loginLogic.CreateClient(connectionString);
        }

        private void buttonClose_Click(object sender, RoutedEventArgs e)
        {
            //CODE: if userInstance != null give option to logout, logout and shutdown, cancel.
            //CODE: if userInstance == null (else) give option to shutdown or cancel
        }

        private void Logout()
        {
            UserInstance.Logout();
            frameContent.Source = new Uri("../Login/LoginPage.xaml", UriKind.Relative);
        }

        private void Shutdown()
        {
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            UserLogic logic = new UserLogic();
            logic.AddUserTest();
        }
    }
}
