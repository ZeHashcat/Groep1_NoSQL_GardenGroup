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
    /// Interaction logic for LoginPasswordResetPage.xaml
    /// </summary>
    public partial class LoginPasswordResetPage : Page
    {
        public LoginPasswordResetPage()
        {
            InitializeComponent();
            CreateClient(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);

            //Placing form in center;
            //NOTE: Please note that the code that once stood here has been pirated away and placed in both windows. Thx!
            //Make shure buttin is disabeled when booting
            ResetPasswordEmailButton.IsEnabled = !string.IsNullOrEmpty(resetPasswordEmailTextBox.Text);

            //User should not be able to change password before email is checked.
            //NOTE: ↓↓↓If these things need to be hidden at initialisation, consider putting "Visibility="Hidden"" inside the tags in the .xaml instead.
            notNeededLabel3.Visibility = Visibility.Hidden;
            notNeededLabel4.Visibility = Visibility.Hidden;
            resetPasswordPaswrdBox.Visibility = Visibility.Hidden;
            resetPasswordRepeatPaswrdBx.Visibility = Visibility.Hidden;
            NewPasswordButton.Visibility = Visibility.Hidden; 
        }

        private void CreateClient(string connectionString)
        {
            UserLogic loginLogic = new UserLogic();
            loginLogic.CreateClient(connectionString);
        }


        private void resetPasswordEmailTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ResetPasswordEmailButton.IsEnabled = !string.IsNullOrEmpty(resetPasswordEmailTextBox.Text);
        }


        private void ResetPasswordEmailButton_Click(object sender, RoutedEventArgs e)
        {
            //Read entered email.                 
            string email = resetPasswordEmailTextBox.Text;
            //Here we call on the Database to check if email exists.
            UserLogic loginLogic = new UserLogic();

            try
            {
                if (loginLogic.CheckEmail(email))
                {

                    //This is for the layout.
                    resetPasswordButtonPressedLbl.Foreground = Brushes.Green;
                    resetPasswordButtonPressedLbl.Content = "Email has been accepted. Please enter new password below.";

                    //Enable user to change password (showing labels and boxes)
                    notNeededLabel3.Visibility = Visibility.Visible;
                    notNeededLabel4.Visibility = Visibility.Visible;
                    resetPasswordPaswrdBox.Visibility = Visibility.Visible;
                    resetPasswordRepeatPaswrdBx.Visibility = Visibility.Visible;
                    NewPasswordButton.Visibility = Visibility.Visible;

                    //We need the email to stay the same now.
                    resetPasswordEmailTextBox.IsEnabled = false;
                    MessageBox.Show("email kan worden opgehaald!!!");
                }
                else
                {
                    resetPasswordButtonPressedLbl.Foreground = Brushes.Red;
                    resetPasswordButtonPressedLbl.Content = "Unknown email has been entered. Please enter a valid email.";
                    MessageBox.Show("je hebt dit verkloot!!!");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void NewPasswordButton_Click(object sender, RoutedEventArgs e)
        {
            string newPassword = resetPasswordPaswrdBox.Password.ToString();
            string newPasswordRepeat = resetPasswordRepeatPaswrdBx.Password.ToString();
            string email = resetPasswordEmailTextBox.Text;

            UserLogic loginLogic = new UserLogic();

            try
            {
                if (newPassword == newPasswordRepeat && 8 < newPassword.Length)
                {
                    //Save the new password. \/ \/ \/ \/ \/ \/ \/ \/ \/ \/ \/ (doe dat hier)
                    //-------------------------------------------------------------------------
                    HashingWithSaltHasher hasher = new HashingWithSaltHasher();
                    HashWithSaltResult hashWithSalt = hasher.HashWithSalt(newPassword, 64, SHA512.Create());
                    //Als ik dit nodig heb schrijf ik result.Hash of .Salt == alleen in deze methode
                    if (loginLogic.ChangePassword(email, hashWithSalt))
                    {
                        MessageBox.Show("Lekker bezig, is gelukt");

                        resetPasswordConfirmLabel.Foreground = Brushes.Green;
                        resetPasswordConfirmLabel.Content = "Congratulations. Your new password has been saved.";
                        MessageBox.Show("Congratulations. Your new password has been saved.");
                    }
                    else
                        MessageBox.Show("WTF ben jij aan het doen, mafkees");

                }
                else
                {
                    resetPasswordConfirmLabel.Foreground = Brushes.Red;
                    resetPasswordConfirmLabel.Content = "The entered passwords dont match.\nKeep in mind the password must contain atleast 8 symbols.";
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }            
        }

        public bool AddUser()
        {
            //We will first check if the entered username and Email dont match the existing ones.
            UserLogic userLogic = new UserLogic();

            if (userLogic.AddUser())
            {
                return true;
            }
            else
                return false;
        }
    }
}
