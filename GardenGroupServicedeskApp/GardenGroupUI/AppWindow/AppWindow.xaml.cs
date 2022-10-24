using GardenGroupLogic;
using GardenGroupModel;
using System;
using System.Windows;

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
            frameContent.Source = new Uri("../Login/LoginPage.xaml", UriKind.Relative);
            //frameContent.Source = new Uri("../Login/LoginPasswordResetPage.xaml", UriKind.Relative);
            //frameContent.Source = new Uri("../UserManagement/AddUserPage.xaml", UriKind.Relative);
            //frameContent.Source = new Uri("../UserManagement/UserManagementPage.xaml", UriKind.Relative);
            //frameContent.Source = new Uri("AppMainEmployeePage.xaml", UriKind.Relative);
            //frameContent.Source = new Uri("AppMainServiceDeskEmployeePage.xaml", UriKind.Relative);            
        }

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

        //AddUserTest bestaat niet
        /*private void Button_Click(object sender, RoutedEventArgs e)
        {
            UserLogic logic = new UserLogic();
            logic.AddUserTest();
        }*/
    }
}
