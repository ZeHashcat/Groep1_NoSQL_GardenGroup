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
        private UserLogic loginLogic = new UserLogic();
        public LoginPasswordResetPage()
        {
            InitializeComponent();

            CreateClient(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
                        
            //Make shure button is disabeled when booting
            ResetPasswordEmailButton.IsEnabled = !string.IsNullOrEmpty(resetPasswordEmailTextBox.Text);

            //User should not be able to change password before email is checked.            
        }

        private void CreateClient(string connectionString)
        {            
            loginLogic.CreateClient(connectionString);
        }

        private void resetPasswordEmailTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ResetPasswordEmailButton.IsEnabled = !string.IsNullOrEmpty(resetPasswordEmailTextBox.Text);
        }

        private void ResetPasswordEmailButton_Click(object sender, RoutedEventArgs e)
        {           
            //Before the user can be allowed to enter the new password they are asked to enter a email to enshure they are the user they make out to be.
            //(In a real application the entered email must first het accepted in the users email)
            
            try
            {
                //Read entered email.
                string email = resetPasswordEmailTextBox.Text;

                //Here we call on the Database to check if email exists.
                if (loginLogic.CheckEmail(email))
                {

                    //This is for the layout.
                    resetPasswordButtonPressedLbl.Foreground = Brushes.Green;
                    resetPasswordButtonPressedLbl.Content = "Email has been accepted. Please enter new password below.";

                    //Enable user to change password (showing labels and boxes)
                    notNeededLabel3.Visibility = Visibility.Visible;
                    notNeededLabel4.Visibility = Visibility.Visible;
                    NewPasswordButton.Visibility = Visibility.Visible;
                    resetPasswordPaswrdBox.Visibility = Visibility.Visible;
                    resetPasswordRepeatPaswrdBx.Visibility = Visibility.Visible;

                    //We need the email to stay the same now.
                    resetPasswordEmailTextBox.IsEnabled = false;
                    MessageBox.Show("Email has been send to validate.");
                }
                else
                {
                    resetPasswordButtonPressedLbl.Foreground = Brushes.Red;
                    resetPasswordButtonPressedLbl.Content = "Unknown email has been entered. Please enter a valid email.";
                    MessageBox.Show("Entered wrong email, please try again");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }            
        }

        private void NewPasswordButton_Click(object sender, RoutedEventArgs e)
        {                       
            //The new password has been entered. It will now get checked and if everything is alright it will get updated into the database
            try
            {
                string email = resetPasswordEmailTextBox.Text;
                string newPassword = resetPasswordPaswrdBox.Password.ToString();
                string newPasswordRepeat = resetPasswordRepeatPaswrdBx.Password.ToString();

                if (newPassword == newPasswordRepeat && 8 < newPassword.Length)
                {
                    //Save the new password.                     
                    HashingWithSaltHasher hasher = new HashingWithSaltHasher();
                    HashWithSaltResult hashWithSalt = hasher.NewHashWithSalt(newPassword, 64, SHA512.Create());
                    //Als ik dit nodig heb schrijf ik result.Hash of .Salt == alleen in deze methode
                    if (loginLogic.ChangePassword(email, hashWithSalt))
                    {                      
                        resetPasswordConfirmLabel.Foreground = Brushes.Green;
                        resetPasswordConfirmLabel.Content = "Congratulations. Your new password has been saved.";
                        MessageBox.Show("Congratulations. Your new password has been saved.");
                    }
                    else
                        throw new Exception("Something went wrong, please try again");
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
    }
}
