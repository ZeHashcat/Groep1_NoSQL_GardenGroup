using GardenGroupModel;
using System;
using System.Windows;

namespace GardenGroupUI
{
    /// <summary>
    /// Interaction logic for AppMenuWindow.xaml
    /// </summary>
    public partial class AppMenuWindow : Window
    {
        public AppMenuWindow(string menuToOpen)
        {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            frameContent.Source = new Uri(menuToOpen, UriKind.Relative);
        }        

        private void buttonClose_Click(object sender, RoutedEventArgs e)
        {
            string message = "";
            string title = "Close Application";
            //CODE: if userInstance != null give option to logout, logout and shutdown, cancel.
            UserInstance userInstance = UserInstance.GetInstance();

            if (userInstance != null)
            {
                message = "You are trying to close this application. Do you want to go to the Login screen? Yes or No?";
                var result = MessageBox.Show(message, title, MessageBoxButton.YesNoCancel);
                //User will go back to login page.
                if (result == MessageBoxResult.Yes)
                {
                    UserInstance.Logout();                    
                    AppMenuWindow appWindow = new AppMenuWindow("../Login/LoginPage.xaml");
                    appWindow.Show();
                    AppWindow.GetWindow(this).Close();
                }
                //The application will be closed.
                else if (result == MessageBoxResult.No)
                {
                    Application.Current.Shutdown();
                }
                //closes messagebox.
                
            }
            //CODE: if userInstance == null (else) give option to shutdown or cancel
            else
            {
                message = "You are trying to close this application. Do you want to close this application?";
                

                var result = MessageBox.Show(message, title,MessageBoxButton.YesNo);
                //User will go back to login page.
                if (result == MessageBoxResult.Yes)
                {
                    //The application will be closed.
                    Application.Current.Shutdown();
                }                
                //Messagebox will be closed.          
            }            
        }
    }
}
