using System.Windows;
using System.Windows.Controls;

namespace GardenGroupUI.UserManagement
{
    /// <summary>
    /// Interaction logic for UserManagementPage.xaml
    /// </summary>
    public partial class UserManagementPage : Page
    {
        public UserManagementPage()
        {
            InitializeComponent();
        }



        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AppMenuWindow appWindow = new AppMenuWindow("../UserManagement/UserManagementPage.xaml");
            appWindow.Show();
        }
    }
}
