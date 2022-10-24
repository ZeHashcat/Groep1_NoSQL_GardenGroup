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
            //CODE: if userInstance != null give option to logout, logout and shutdown, cancel.
            //CODE: if userInstance == null (else) give option to shutdown or cancel
        }
    }
}
