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
using MongoDB.Driver;
using MongoDB.Bson;

namespace GardenGroupUI
{
    /// <summary>
    /// Interaction logic for TestWindow.xaml
    /// </summary>
    public partial class TestWindow : Window
    {
        //NOTE: All this code prolly belongs in DAO I think, this is just an example on an example branch.
        private MongoClient client;
        //NOTE: ↓↓↓↓↓-I think this establishes a connection with the database.
        private IMongoDatabase database = null;
        //NOTE: ↓↓↓↓↓-I think this establishes a connection with a collection within the database.
        private IMongoCollection<BsonDocument> collection = null;

        //EXPLANATION: This is just an example to show how to create a BsonDocument, you can leave away the new Bson<class>() on each line and input the variable directy.
        //NOTE: could potentially lead to problems where multiple types of Bson<class> are possible and it picks the wrong one.
        //NOTE: For example, { "_id", 1 } should work, since it accepts variables as a BsonValue which should automatically convert it to an BsonInt32.
        private BsonDocument user = new BsonDocument
            {
                { "_id", new BsonInt32(1) }, //EXPLANATION: <--- Leave this out and it will assign an _id automatically, not sure what pro's and cons are for auto and manual.
                { "User Id", new BsonInt32(12345) }, //EXPLANATION: <----Actual employeenumber
                { "First Name", new BsonString("Mylo") },
                { "Last Name", new BsonString("Bronkhorst") },
                { "Password", new BsonString("thisIsTotallyHashedRn") }, //NOTE: <----Should already be hashed and salted at this point.
                { "Acount_isConnected", new BsonBoolean(false) }, //NOTE: Not gonna need this, just to illustrate the bool.
                { "Role", new BsonString("Sudo") }, //NOTE*: A clear system for privileges is needed, See "NOTE*:" Below.
                { "Teams", new BsonDocument
                    {
                        { "Team1", new BsonInt32(3) },
                        { "Team2", new BsonInt32(7) },
                        //NOTE: etc.
                    }
                }
            };

        //NOTE*: I had the idea to hardcode the "Sudo" role into this app and have roles such as admin, employee and user defined as docs in the database.
        //NOTE*: There should always be at least one Sudo role user in the entire database. Only a Sudo role user may CRUD itself and other Sudo's.
        //NOTE*: This way, new roles could potentially be defined and assigned within the app.
        //NOTE*: So when this app checks for privileges it would query the following document for example.
        private BsonDocument employee = new BsonDocument
            {
                { "_id", new BsonString("Employee") }, //EXPLANATION: <--- Role as _id so it is unique.
                { "User", new BsonDocument //EXPLANATION: <---Refers to a Collection named User.
                    {   //EXPL./QUE.:↓↓↓↓↓-CRUD rights, anything else needed?
                        { "Create", new BsonBoolean(false) }, //QUESTION: <---- Should this be false?
                        { "Read", new BsonBoolean(true) },
                        { "Update", new BsonBoolean(true) },
                        { "Delete", new BsonBoolean(false) },
                    }
                },
                { "Ticket", new BsonDocument //EXPLANATION: <---Refers to a Collection named Ticket.
                    {   //EXPL./QUE.:↓↓↓↓↓-CRUD rights, anything else needed?
                        { "Create", new BsonBoolean(true) },
                        { "Read", new BsonBoolean(true) },
                        { "Update", new BsonBoolean(true) },
                        { "Delete", new BsonBoolean(true) },
                    }
                }
            };

        public TestWindow(MongoClient client)
        {
            this.client = client; // Client is given from MainWindow
            InitializeComponent();
        }

        private void buttonSetDatabase_Click(object sender, RoutedEventArgs e)
        {
            string databaseName = textBoxSetDatabase.Text;
            labelCurrentDatabase.Content = databaseName;
            database = client.GetDatabase(databaseName);        //NOTE: Doing this establishes a connection with the database I think.   
        }

        private void buttonSetCollection_Click(object sender, RoutedEventArgs e)
        {
            string collectionName = textBoxSetCollection.Text;
            labelCurrentCollection.Content = collectionName;
            collection = database.GetCollection<BsonDocument>(collectionName); //NOTE: Doing this establishes a connection with a collection within the database I think.
        }

        private void buttonCreateUser_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                collection.InsertOne(user); //EXPLANATION: This actually inserts a document into the database.
                MessageBox.Show("Document inserted!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonFindUser_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                FilterDefinition<BsonDocument> filter = Builders<BsonDocument>.Filter.Eq("firstName", "Mylo"); //EXPLANATION: Filters documents on key firstname with value "Mylo" only, a collection must be set at this point.
                BsonDocument document = collection.Find(filter).First(); //EXPLANATION: Finds and returns first document with filter applied.
                textBlockDocumentOutput.Text = document.ToString();
            }
            catch (Exception ex)
            {
                textBlockDocumentOutput.Text = ex.Message;
            }
        }

        private void buttonCreateRole_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                collection.InsertOne(user); //EXPLANATION: This actually inserts a document into the database.
                MessageBox.Show("Document inserted!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonFindRole_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                FilterDefinition<BsonDocument> filter = Builders<BsonDocument>.Filter.Eq("_id", "Employee"); //EXPLANATION: Filters documents on key "_id" with value "12345" only, a collection must be set at this point.
                BsonDocument document = collection.Find(filter).First(); //EXPLANATION: Finds and returns first document with filter applied. since it filters on an unique id this will only ever return one document.
                textBlockDocumentOutput.Text = document.ToString();
            }
            catch (Exception ex)
            {
                textBlockDocumentOutput.Text = ex.Message;
            }
        }
    }
}
