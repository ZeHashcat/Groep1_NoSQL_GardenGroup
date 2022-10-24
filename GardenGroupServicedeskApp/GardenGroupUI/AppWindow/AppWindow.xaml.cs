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
            //MessageBox.Result
            var result = MessageBox.Show("You are trying to close this application. Do you want to close the application?", "Close application", MessageBoxButton.YesNo);
            //User will go back to login page.
            if (result == MessageBoxResult.Yes)
            {
                //The application will be closed.
                Shutdown();
            }
            //Closes messagebox.            
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
