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
using MongoDB.Driver;
using GardenGroupModel;
//using GardenGroupLogic; Commented out because of a lack of classes. Will need later tho.

namespace GardenGroupUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// Also, this needs to be the starting point
    /// </summary>
    public partial class MainWindow : Window
    {
        //IDEA: Everytime a user logs in or logs out, save data like username and a timestamp (maybe ip?) to a BSON in a collection.
        //NOTE: Now you have an instant check if the database is working on login.

        //NOTE: Settings should directly be written to .ini when modified.
        //NOTE: .ini only saves connectionstring name, actual password and user data are protected within App.config, fact check this <<---`-`/\/\`-`(REMINDER)
        //NOTE: If writing to .ini fails, catch it.
        //NOTE: Checkbox will write to bool [AUTORUN] in .ini when Launch is clicked

        //NOTE: Move these 2 strings somewhere more relevant, not as globals here I guess.
        //NOTE: Possibly have a config menu where it is possible to select/add/remove connectionstrings. ADDENDUM: Definitely*
        private string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        private string name = "\"G.G.\" Datacenter-01 - Frankfurt (MongoDB)";

        //QOUTE: "Typically you only create one MongoClient instance for a given cluster and use it across your application." 
        // - https://mongodb.github.io/mongo-csharp-driver/2.17/getting_started/quick_tour/
        
        Settings settings = new Settings(SystemParameters.PrimaryScreenWidth, SystemParameters.PrimaryScreenHeight);

        public MainWindow()
        {
            InitializeComponent();
        }

        private void buttonLaunch_Click(object sender, RoutedEventArgs e)
        {
            //QUESTION: Is there not an inbuild function that does what ClientSetup does? <<---`-`/\/\`-`(REMINDER)
            MongoClient client = Client.ClientSetup(connectionString, name);
            //NOTE: Look into WPF pages, maybe only one window is needed.
            //NOTE: Code below will eventually lead into Login.
            //ENTRY_POINT: Replace below with your own window. <<---`-`/\/\`-`(IMPORTANT)
            TestWindow testWindow = new TestWindow();
            //testWindow.Show();
            Login login = new Login();
            login.Show();
            this.Hide();
        }

        private void DetectResolution()
        {
            
        }
    }
}
