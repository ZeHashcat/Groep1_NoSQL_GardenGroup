﻿using GardenGroupLogic;
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
        }

        private void CreateClient(string connectionString)
        {
            UserLogic loginLogic = new UserLogic();
            loginLogic.CreateClient(connectionString);
        }

        private void buttonClose_Click(object sender, RoutedEventArgs e)
        {            
            //MessageBox.Result
            MessageBoxResult result = MessageBox.Show("You are trying to close this application. Do you want to close the application?", "Close application", MessageBoxButton.YesNo);
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
    }
}
