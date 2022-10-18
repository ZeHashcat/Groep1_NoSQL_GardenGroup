using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
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
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        private UserLogic loginLogic = new UserLogic();
        public LoginPage()
        {
            InitializeComponent();

            CreateClient(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            GetLoginInfo();
        }

        //-------------------------------------------------------------------------------------
        //-------------------------------------------------------------------------------------
        //Login has been presses. Checking if the username and password are correct. 
        //If password incorrect retry.
        //If password correct send to respectable window.
        private void loginLoginButton_Click(object sender, RoutedEventArgs e)
        {
            //Stuur hier door naar Dashboard. (Mylo)
            
            string username = loginUsernameTextBox.Text;
            string password = loginPasswordBox.Password;
                        
           
            try
            {
                if (loginLogic.CheckLogin(username, password))
                {
                    //Creating user instance
                    User user = loginLogic.GetUser(username);
                    UserInstance.GetUserInstance(user);
                    //change source of window to AppMainPage.xaml <----------------------------


                    MessageBox.Show($"Welcome {user.FirstName}");
                }
                else
                {

                }
                //MessageBox.Show("je hebt dit verkloot!!!");

                loginWrongPasswordMessageLabel.Foreground = new SolidColorBrush(Colors.Red);
                loginWrongPasswordMessageLabel.Content = "Wrong username or password entered,\nPlease try again.";

                if (loginRememberMeCheckBox.IsChecked ?? false)
                {
                    SetLoginInfo();
                }
                else
                {
                    DeleteLoginInfo();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }            

        }

        private void CreateClient(string connectionString)
        {           
            loginLogic.CreateClient(connectionString);
        }

        private void SetLoginInfo()
        {
            string keyUsername = "username";
            string keyPassword = "password";
            try
            {
                Configuration appConfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                KeyValueConfigurationCollection settings = appConfig.AppSettings.Settings;
                if (settings[keyUsername] == null)
                {
                    settings.Add(keyUsername, loginUsernameTextBox.Text);
                }
                else
                {
                    settings[keyUsername].Value = loginUsernameTextBox.Text;
                }
               
                if (settings[keyPassword] == null)
                {
                    settings.Add(keyPassword, loginPasswordBox.Password);
                }
                else
                {
                    settings[keyPassword].Value = loginPasswordBox.Password;
                }
                appConfig.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(appConfig.AppSettings.SectionInformation.Name);
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Something went wrong: {ex.Message}");
            }
        }

        private void GetLoginInfo()
        {
            
            if(ConfigurationManager.AppSettings["username"] != null && ConfigurationManager.AppSettings["password"] != null)
            {
                loginUsernameTextBox.Text = ConfigurationManager.AppSettings["username"];
                loginPasswordBox.Password = ConfigurationManager.AppSettings["password"];

                loginRememberMeCheckBox.IsChecked = true;
            }
        }

        private void DeleteLoginInfo()
        {
            string keyUsername = "username";
            string keyPassword = "password";
            try
            {
                Configuration appConfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                KeyValueConfigurationCollection settings = appConfig.AppSettings.Settings;
                
                settings[keyUsername].Value = null;
                settings[keyPassword].Value = null;

                appConfig.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(appConfig.AppSettings.SectionInformation.Name);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Something went wrong: {ex.Message}");
            }
        }

        //-------------------------------------------------------------------------------------
        //-------------------------------------------------------------------------------------
        //User has forgotten their password. User enters email.
        //If email exists (and has been accepted) user can reset password.
        //New Password will be saved.
        private void LoginForgotLoginButton_Click(object sender, RoutedEventArgs e)
        {
              
        }
    }
}
