using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Policy;
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

namespace GardenGroupUI.UserManagement
{
    /// <summary>
    /// Interaction logic for AddUserPage.xaml
    /// </summary>
    public partial class AddUserPage : Page
    {
        private UserLogic userLogic = new UserLogic();
        private LocationService locationService = new LocationService();
        private RoleService roleService = new RoleService();

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
                List<string> locations = await Task.Run(() => locationService.GetLocations());
                foreach (string location in locations)
                {
                    addUserLocationsComboBox.Items.Add(location);
                }

                List<string> roles = await Task.Run(() => roleService.GetRoles());
                foreach (string role in roles)
                {
                    addUserRoleComboBox.Items.Add(role);
                }
            }            
            catch (Exception ex)
            {
                MessageBox.Show($"Locations cant be loaded: {ex.Message}");
            }
        }

        private void AddUserButton_Click(object sender, RoutedEventArgs e)
        {
            //Roep hier database aan en voeg toe. Gebruik try catch.
            //Letop dat de email en gebruikersnaam niet twee maal voor mogen komen!!!

            

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

                //Change input into hash and salt
                HashingWithSaltHasher hasher = new HashingWithSaltHasher();
                HashWithSaltResult hashWithSalt = hasher.HashWithSalt(password, 64, SHA512.Create());

                if (userLogic.AddUser(username, hashWithSalt, firstname, lastname, email, phoneNumber, role, location))
                {
                    MessageBox.Show("The new user has been added.");
                }
                else
                    MessageBox.Show("A user already exists with the same USERNAME, E-MAIL AND OR PHONE NUMBER");
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CancelUserAddButton_Click(object sender, RoutedEventArgs e)
        {
            
            //Terug naar usermannagement Window <------- <-------------
            //to usermanagement
            
        }  

    }
}
