﻿using System;
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

            if (loginLogic.CheckEmail(email))
            {
                //Creating user instance
                /*User user = loginLogic.GetUser(email);
                UserInstance.GetUserInstance(user);*/
                //change source of window to AppMainPage.xaml <----------------------------



                //This is for the layout.
                resetPasswordButtonPressedLbl.Foreground = Brushes.Green;
                resetPasswordButtonPressedLbl.Content = "Email has been accepted. Please enter new password below.";

                //Enable user to change password (showing labels and boxes)
                notNeededLabel3.Visibility = Visibility.Visible;
                notNeededLabel4.Visibility = Visibility.Visible;
                resetPasswordPaswrdBox.Visibility = Visibility.Visible;
                resetPasswordRepeatPaswrdBx.Visibility = Visibility.Visible;
                NewPasswordButton.Visibility = Visibility.Visible;
                MessageBox.Show("email kan worden opgehaald!!!");
            }
            else
            {
                resetPasswordButtonPressedLbl.Foreground = Brushes.Red;
                resetPasswordButtonPressedLbl.Content = "Unknown email has been entered. Please enter a valid email.";
                MessageBox.Show("je hebt dit verkloot!!!");
            }
            


            if (email == "mark")
            {
                
            }
            else
            {
                
            }
        }

        private void NewPasswordButton_Click(object sender, RoutedEventArgs e)
        {
            string newPassword = resetPasswordPaswrdBox.Password.ToString();
            string newPasswordRepeat = resetPasswordRepeatPaswrdBx.Password.ToString();

            if (newPassword == newPasswordRepeat && 8 < newPassword.Length)
            {
                //Save the new password. \/ \/ \/ \/ \/ \/ \/ \/ \/ \/ \/ (doe dat hier)
                //-------------------------------------------------------------------------
                HashingWithSaltHasher hasher = new HashingWithSaltHasher();
                HashWithSaltResult result = hasher.HashWithSalt(newPassword, 64, SHA512.Create());
                //Als ik dit nodig heb schrijf ik result.Hash of .Salt == alleen in deze methode


                resetPasswordConfirmLabel.Foreground = Brushes.Green;
                resetPasswordConfirmLabel.Content = "Congratulations. Your new password has been saved.";
                MessageBox.Show("Congratulations. Your new password has been saved.");

                
            }
            else
            {
                resetPasswordConfirmLabel.Foreground = Brushes.Red;
                resetPasswordConfirmLabel.Content = "The entered passwords dont match.\nKeep in mind the password must contain atleast 8 symbols.";
            }
        }
    }
}
