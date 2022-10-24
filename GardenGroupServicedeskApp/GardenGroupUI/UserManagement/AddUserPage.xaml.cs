using GardenGroupLogic;
using GardenGroupModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace GardenGroupUI.UserManagement
{
    /// <summary>
    /// Interaction logic for AddUserPage.xaml
    /// </summary>
    public partial class AddUserPage : Page
    {
        private UserLogic userLogic = new UserLogic();
        private RoleService roleService = new RoleService();
        private LocationService locationService = new LocationService();

        public AddUserPage()
        {
            InitializeComponent();
            CreateClient(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
        }

        private void CreateClient(string connectionString)
        {
            userLogic.CreateClient(connectionString);
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            FillComboBoxes();
        }

        private async void FillComboBoxes()
        {
            try
            {
                addUserRoleComboBox.Items.Clear();

                List<string> roles = await Task.Run(() => roleService.GetRoles());
                foreach (string role in roles)
                {
                    addUserRoleComboBox.Items.Add(role);
                }

                addUserLocationsComboBox.Items.Clear();

                List<string> locations = await Task.Run(() => locationService.GetLocations());
                foreach (string location in locations)
                {
                    addUserLocationsComboBox.Items.Add(location);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Locations cant be loaded: {ex.Message}");
            }
        }

        private void AddUserButton_Click(object sender, RoutedEventArgs e)
        {
            //Here we call on the database using try catch
            //The username, email and phone number cant be the same as one that is already in the database.

            try
            {
                //Getting data from the inputboxes 
                string firstname = addUserFirstnameTextBox.Text;
                string lastname = addUserlastnameTextBox.Text;
                string role = addUserRoleComboBox.Text;
                string email = addUserEmailTextBox.Text;
                double phoneNumber = double.Parse(addUserPhonenumberTextBox.Text);
                string location = addUserLocationsComboBox.Text;
                string username = addUserUsernameTextBox.Text;
                string password = addUserPasswordTextBox.Text;

                if (!IsNullOrEmpty(firstname, lastname, email, phoneNumber, username, password, role, location))
                {
                    //Change input into hash and salt
                    HashingWithSaltHasher hasher = new HashingWithSaltHasher();
                    HashWithSaltResult hashWithSalt = hasher.NewHashWithSalt(password, 64, SHA512.Create());

                    if (userLogic.AddUser(username, hashWithSalt, firstname, lastname, email, phoneNumber, role, location))
                    {
                        MessageBox.Show("The new user has been added.");
                    }
                    else
                        MessageBox.Show("A user already exists with the same USERNAME, E-MAIL AND OR PHONE NUMBER");
                }
                else
                {
                    MessageBox.Show("Some fields might not be filled.");
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CancelUserAddButton_Click(object sender, RoutedEventArgs e)
        {

            AppMenuWindow appWindow = new AppMenuWindow("../UserManagement/UserManagementPage.xaml");
            appWindow.Show();
            AppWindow.GetWindow(this).Close();

        }  

        private bool IsNullOrEmpty(string firstname, string lastname, string email, double phoneNumber, string username, string password, string role, string location)
        {
            if (firstname == "" || lastname == "" || email == "" || phoneNumber.ToString() == "" || username == "" || password == "" || role == "" || location == "")
            {
                return true;
            }
            else
                return false;
        }
    }
}
