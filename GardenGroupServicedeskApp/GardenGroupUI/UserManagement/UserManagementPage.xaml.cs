using GardenGroupLogic;
using GardenGroupModel;
using System.Configuration;
using System.Windows;
using System.Windows.Controls;

namespace GardenGroupUI.UserManagement
{
    /// <summary>
    /// Interaction logic for UserManagementPage.xaml
    /// </summary>
    public partial class UserManagementPage : Page
    {
        UserLogic userLogic = new UserLogic();

        public UserManagementPage()
        {
            InitializeComponent();

            CreateClient(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);

            FillDataGrid();
        }

        private void CreateClient(string connectionString)
        {
            userLogic.CreateClient(connectionString);
        }

        private void FillDataGrid()
        {
            int id = 1;
            foreach (User user in userLogic.GetAllusers())
            {                
                DataGridUserOverview.Items.Add(new { Id = id, Username = user.UserName.Value, FirstName = user.FirstName.Value, LastName = user.LastName.Value, Email = user.Email.Value});
                id++;
            }
        }

        private void AddUserButton_Click(object sender, RoutedEventArgs e)
        {
            AppMenuWindow appWindow = new AppMenuWindow("../UserManagement/AddUserPage.xaml");
            appWindow.Show();
            AppWindow.GetWindow(this).Close();
        }
    }
}
