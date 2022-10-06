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
    /// Interaction logic for AddUserWindow.xaml
    /// </summary>
    public partial class AddUserWindow : Window
    {
        public AddUserWindow()
        {
            InitializeComponent();
        }

        private void AddUserButton_Click(object sender, RoutedEventArgs e)
        {
            //Roep hier database aan en voeg toe. Gebruik try catch.
            //Letop dat de email en gebruikersnaam niet twee maal voor mogen komen!!!

        }

        private void CancelUserAddButton_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();

            //Terug naar ..... Window <------- <-------------

            this.Close();
        }
    }
}
