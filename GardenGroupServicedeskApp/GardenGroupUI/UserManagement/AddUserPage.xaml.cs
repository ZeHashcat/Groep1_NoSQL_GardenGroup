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

namespace GardenGroupUI.UserManagement
{
    /// <summary>
    /// Interaction logic for AddUserPage.xaml
    /// </summary>
    public partial class AddUserPage : Page
    {
        public AddUserPage()
        {
            InitializeComponent();
        }

        private void AddUserButton_Click(object sender, RoutedEventArgs e)
        {
            //Roep hier database aan en voeg toe. Gebruik try catch.
            //Letop dat de email en gebruikersnaam niet twee maal voor mogen komen!!!
            try
            {
                /*string firstname = addUserFirstnameTextBox.Text;
                string lastname = addUserlastnameTextBox.Text;
                //enum role;
                string email = addUserEmailTextBox.Text;
                int phoneNumber = int.Parse(addUserPhonenumberTextBox.Text);

                string username = addUserUsernameTextBox.Text;
                string password = addUserPasswordTextBox.Text;*/
                //enum location;
                //User user = new User(firstname, lastname, role, email, phoneNumber, username, password, location);

                UserLogic userLogic = new UserLogic();
                if (userLogic.AddUser())
                {
                    MessageBox.Show("Lekker gedaan, een gebruiker is toegevoegd");
                }
                else
                    MessageBox.Show("Man er gaat iets fout, Fix het mongool");
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            


        }

        private void CancelUserAddButton_Click(object sender, RoutedEventArgs e)
        {
            
            //Terug naar usermannagement Window <------- <-------------
            
        }
    }
}
