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
using System.Windows.Navigation;
using System.Windows.Shapes;

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
            string firstname = addUserFirstnameTextBox.Text;
            string lastname;
            //enum typeUser;
            string email;
            int phoneNumber;

            string username;
            string password;
            //enum location;



        }

        private void CancelUserAddButton_Click(object sender, RoutedEventArgs e)
        {
            
            //Terug naar usermannagement Window <------- <-------------
            
        }
    }
}
