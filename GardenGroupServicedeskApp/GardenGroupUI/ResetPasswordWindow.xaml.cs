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

namespace GardenGroupUI
{
    /// <summary>
    /// Interaction logic for ResetPasswordWindow.xaml
    /// </summary>
    public partial class ResetPasswordWindow : Window
    {
        public ResetPasswordWindow()
        {
            InitializeComponent();
            //Placing form in center;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            //Make shure buttin is disabeled when booting
            ResetPasswordEmailButton.IsEnabled = !string.IsNullOrEmpty(resetPasswordEmailTextBox.Text);

            //User should not be able to change password before email is checked.
            notNeededLabel3.Visibility = Visibility.Hidden;
            notNeededLabel4.Visibility = Visibility.Hidden;
            resetPasswordPaswrdBox.Visibility = Visibility.Hidden;
            resetPasswordRepeatPaswrdBx.Visibility = Visibility.Hidden;
            NewPasswordButton.Visibility = Visibility.Hidden;
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
            //------------------------------------------------------------ <------ <------ Hier moet je door luuk.

            if(email == "mark")
            {
                resetPasswordButtonPressedLbl.Foreground = Brushes.Green;
                resetPasswordButtonPressedLbl.Content = "Email has been accepted. Please enter new password below.";

                //Enable user to change password (showing labels and boxes)
                notNeededLabel3.Visibility = Visibility.Visible;
                notNeededLabel4.Visibility = Visibility.Visible;
                resetPasswordPaswrdBox.Visibility = Visibility.Visible;
                resetPasswordRepeatPaswrdBx.Visibility = Visibility.Visible;
                NewPasswordButton.Visibility = Visibility.Visible;
            }
            else
            {
                resetPasswordButtonPressedLbl.Foreground = Brushes.Red;
                resetPasswordButtonPressedLbl.Content = "Unknown email has been entered. Please enter a valid email.";
            }
        }

        private void NewPasswordButton_Click(object sender, RoutedEventArgs e)
        {
            string newPassword = resetPasswordPaswrdBox.Password.ToString();
            string newPasswordRepeat = resetPasswordRepeatPaswrdBx.Password.ToString();

            if(newPassword == newPasswordRepeat && 8 < newPassword.Length)
            {
                //Save the new password. \/ \/ \/ \/ \/ \/ \/ \/ \/ \/ \/ (doe dat hier)
                //-------------------------------------------------------------------------
                resetPasswordConfirmLabel.Foreground = Brushes.Green;
                resetPasswordConfirmLabel.Content = "Congratulations. Your new password has been saved.";
                MessageBox.Show("Congratulations. Your new password has been saved.");

                this.Hide();
                //Login login = new Login();
                //login.ShowDialog();
                this.Close();
            }
            else
            {
                resetPasswordConfirmLabel.Foreground = Brushes.Red;
                resetPasswordConfirmLabel.Content = "The entered passwords dont match.\nKeep in mind the password must contain atleast 8 symbols.";
            }
        }
    }
}
