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

namespace GardenGroupUI
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        //NOTE: These two variables are unnecessary, make them local to the methods or read directly from Passwordbox.
        private string username;
        private string password;

        public LoginPage()
        {
            InitializeComponent();
            //Load window in the center.
            //NOTE: Please note that the code that once stood here has been pirated away and placed in both windows. Thx!
        }

        //-------------------------------------------------------------------------------------
        //-------------------------------------------------------------------------------------
        //Login has been presses. Checking if the username and password are correct. 
        //If password incorrect retry.
        //If password correct send to respectable window.
        private void loginLoginButton_Click(object sender, RoutedEventArgs e)
        {
            //Stuur hier door naar Dashboard. (Mylo)
            /*if(loginRememberMeLabel.IsChecked)
            {

            }*/
            username = loginUsernameTextBox.Text;
            password = loginPasswordBox.Password;
            LoginLogic loginLogic = new LoginLogic();
            loginLogic.CheckLogin(username, password);

        }

        //-------------------------------------------------------------------------------------
        //-------------------------------------------------------------------------------------
        //User chose not to login. 
        //Close login window.
        private void loginCloseButton_Click(object sender, RoutedEventArgs e)
        {
            //this.Close();
        }

        //-------------------------------------------------------------------------------------
        //-------------------------------------------------------------------------------------
        //User wants to be remembered by system.
        //The system on user device will remember login details.
        private void loginRememberMeLabel_Checked(object sender, RoutedEventArgs e)
        {
            //Onthoud de gebruiker. //Hoef ik niet te gebruiken. ik kan het ergens ander aan roepen.
            //Dit kan wel weg.
        }

        //-------------------------------------------------------------------------------------
        //-------------------------------------------------------------------------------------
        //User has forgotten their password. User enters email.
        //If email exists (and has been accepted) user can reset password.
        //New Password will be saved.
        private void LoginForgotLoginButton_Click(object sender, RoutedEventArgs e)
        {
            //this.Hide();
            ResetPasswordWindow resetPassword = new ResetPasswordWindow();
            resetPassword.ShowDialog();
            //this.Close();
        }
    }
}
