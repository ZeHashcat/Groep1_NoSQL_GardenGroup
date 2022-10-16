using System.Windows.Controls;
using System.Windows;
using System;
using GardenGroupModel;
using System.Collections.Generic;
using GardenGroupLogic;
using System.Xml.Linq;
using MongoDB.Bson;

namespace GardenGroupUI
{
    
    public partial class CRUDPage : Page
    {

        TicketLogic ticketLogic = new TicketLogic();

        public CRUDPage()
        {
            CRUDState state = CRUDState.Read;
            Priority priority = new Priority();
            InitializeComponent();
            switch (state)
            {

                case CRUDState.Create:
                    CreateSetup();
                    break;
                case CRUDState.Read:
                    ReadSetup();
                    break;
                case CRUDState.Update:
                    UpdateSetup();
                    break;
                case CRUDState.Delete:
                    ReadSetup();
                    break;
            }






        }

        public void CreateSetup()
        {

            //fill drop dowms
            //date time selector fill
            DateTime date = DateTime.Now;
            //fill drop down whit priorities

            int lengthPriority = Enum.GetNames(typeof(Priority)).Length;
            for (int i = 0; i < lengthPriority; i++)
            {
                ComboBoxPriority.Items.Add(Enum.GetName(typeof(Priority), i));
            }

            //fill drop down whit IncidentTypes
            int lengthIncidentType = Enum.GetNames(typeof(IncidentType)).Length;
            for (int i = 0; i < lengthIncidentType; i++)
            {
                ComboBoxIncidentType.Items.Add(Enum.GetName(typeof(IncidentType), i));
            }

            DateSelectDeadline.SelectedDate= DateTime.Today.AddDays(7);
        }

        public void UpdateSetup()
        {

        }
        public void ReadSetup()
        {
         

        }

        private void SetupUsers(List<User> users)
        {
           // ComboBoxUser.SetValue(users.);
        }
        private void SetupUsers(User user)
        {

        }

        private void ButtonTestRead_Click(object sender, RoutedEventArgs e)
        {
            String result = "";
            List<Ticket> tickets = ticketLogic.ReadTicket();

            foreach (Ticket ticket in tickets)
            {
                result += ticket.Subject.ToString() + "\n"
                   + ticket.Description.ToString() + "\n";
            }
            MessageBox.Show(result);
        }

       

        private void ButtonTestDelete_Click(object sender, RoutedEventArgs e)
        {
            ticketLogic.DeleteTicket();
        }

     

        private void ButtonTestCreate_Click(object sender, RoutedEventArgs e)
        {
            Ticket ticket = new Ticket()
            {
                DateReported = DateTime.Today,
                Subject = "Create Test",
                Incident = IncidentType.hardware,
                User = new User(
                        new BsonKeyValuePair("id", ObjectId.GenerateNewId()),
                        new BsonKeyValuePair("userName", "Reynard96Blazer"),
                        new BsonKeyValuePair("password", ""),
                        new BsonKeyValuePair("firstName", "Jane"),
                        new BsonKeyValuePair("lastName", "Doe"),
                        new BsonKeyValuePair("role", "test user"),
                        new BsonKeyValuePair("email", "Jane.Doe@Test.com"),
                        new BsonKeyValuePair("phoneNumber","123456789"),
                        new BsonKeyValuePair("location", "testlocation")

                        ),
            Impact = Priority.normal,
            Urgency = Priority.low,
            DeadLine = DateTime.Today.AddDays(1),
            Status = TicketStatus.closed,
            Description ="Dit is een test message 10 days until total destruction of all human kind. thank you for being alive🙇‍♀️ "
            };
            ticketLogic.CreateTicket(ticket);
        }

        private void ButtonTestUpdate_Click(object sender, RoutedEventArgs e)
        {
            Ticket ticket = new Ticket()
            {
                DateReported = DateTime.Today,
                Subject = "Create Test",
                Incident = IncidentType.hardware,
                User = new User(
                       new BsonKeyValuePair("id", "634c05a26f28451bb8619190"),
                       new BsonKeyValuePair("userName", "test"),
                       new BsonKeyValuePair("password", ""),
                       new BsonKeyValuePair("firstName", "Jane"),
                       new BsonKeyValuePair("lastName", "Doe"),
                       new BsonKeyValuePair("role", "test user"),
                       new BsonKeyValuePair("email", "Jane.Doe@Test.com"),
                       new BsonKeyValuePair("phoneNumber", "123456789"),
                       new BsonKeyValuePair("location", "testlocation")

                       ),
                Impact = Priority.normal,
                Urgency = Priority.low,
                DeadLine = DateTime.Today.AddDays(1),
                Status = TicketStatus.closed,
                Description = "Dit is een test message 10 days until total destruction of all human kind. thank you for being alive🙇‍♀️ "
            };
            Ticket update = new Ticket()
            {
                DateReported = DateTime.Today.AddDays(2),
                Subject = "Create Test",
                Incident = IncidentType.hardware,
                User = new User(
                       new BsonKeyValuePair("id", "634c05a26f28451bb8619190"),
                       new BsonKeyValuePair("userName", "Reynard96Blazer"),
                       new BsonKeyValuePair("password", ""),
                       new BsonKeyValuePair("firstName", "Jane"),
                       new BsonKeyValuePair("lastName", "Doe"),
                       new BsonKeyValuePair("role", "test user"),
                       new BsonKeyValuePair("email", "Jane.Doe@Test.com"),
                       new BsonKeyValuePair("phoneNumber", "123456789"),
                       new BsonKeyValuePair("location", "testlocation")

                       ),
                Impact = Priority.high,
                Urgency = Priority.high,
                DeadLine = DateTime.Today.AddDays(3),
                Status = TicketStatus.open,
                Description = "Updated tickets"
            };


            ticketLogic.UpdateTicket(ticket,update);
        }
    }
}
