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
        private IMongoDatabase database = null;
        private IMongoCollection<BsonDocument> collection = null;


        //EXPLANATION: This is just an example to show how to create a BsonDocument, you can leave away the new Bson<class>() on each line and input the variable directy.
        //NOTE: could potentially lead to problems where multiple types of Bson<class> are possible and it picks the wrong one.
        //NOTE: For example, { "_id", 1 } should work, since it accepts variables as a BsonValue which should automatically convert it to an BsonInt32.
        private BsonDocument user = new BsonDocument
            {
                { "_id", new BsonInt32(1) },
                { "userId", new BsonInt32(12345) },
                { "firstName", new BsonString("Mylo") },
                { "lastName", new BsonString("Bronkhorst") },
                { "password", new BsonString("thisIsTotallyHashedRn") },
                { "acountActive", new BsonBoolean(false) },
                { "privileges", new BsonDocument
                    {
                        { "privilege1", new BsonString("User") },
                        { "privilege2", new BsonString("Employee") },
                        { "privilege3", new BsonString("Admin") },
                        { "privilege4", new BsonString("Sudo") },
                    }
                },
                { "teams", new BsonDocument
                    {
                        { "team1", new BsonInt32(3) },
                        { "team2", new BsonInt32(7) },
                    }
                }
            };

        public TestWindow(MongoClient client)
        {
            this.client = client;
            InitializeComponent();
        }

        private void buttonSetDatabase_Click(object sender, RoutedEventArgs e)
        {
            string databaseName = textBoxSetDatabase.Text;
            labelCurrentDatabase.Content = databaseName;
            database = client.GetDatabase(databaseName);           
        }

        private void buttonSetCollection_Click(object sender, RoutedEventArgs e)
        {
            string collectionName = textBoxSetCollection.Text;
            labelCurrentCollection.Content = collectionName;
            collection = database.GetCollection<BsonDocument>(collectionName);
        }

        private void buttonCreateUser_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                collection.InsertOne(user);
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
                FilterDefinition<BsonDocument> filter = Builders<BsonDocument>.Filter.Eq("counter", "Mylo");
                BsonDocument document = collection.Find(filter).First();
                textBlockDocumentOutput.Text = document.ToString();
            }
            catch (Exception ex)
            {
                textBlockDocumentOutput.Text = ex.Message;
            }
        }
    }
}
