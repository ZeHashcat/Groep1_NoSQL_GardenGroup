using System.Windows.Controls;
using System.Windows;
using System;
using GardenGroupModel;
using System.Collections.Generic;
using GardenGroupLogic;
using System.Xml.Linq;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace GardenGroupUI
{
    
    public partial class CRUDPage : Page
    {

        TicketLogic ticketLogic = new TicketLogic();

        Ticket ticket;

        TicketStatus currentStatus = TicketStatus.open;

        public CRUDPage()
        {
            CRUDState state = CRUDState.Create;
            Priority priority = new Priority();
            ticket = ticketLogic.ReadTicket()[0];

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

                ComboBoxImpact.Items.Add(Enum.GetName(typeof(Priority), i));
                ComboBoxUrgency.Items.Add(Enum.GetName(typeof(Priority), i));

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
            this.IsEnabled = false;
            
             DatePickerDateTime.SelectedDate = ticket.DateReported;
            

             TextBoxSubject.Text = ticket.Subject;


             ComboBoxIncidentType.Items.Add (ticket.Incident);
            ComboBoxIncidentType.SelectedIndex = 0;

             ComboBoxUser.Items.Add(ticket.User.FirstName.Value.ToString());
            ComboBoxUser.SelectedIndex = 0;



            ComboBoxImpact.Items.Add(ticket.Impact.ToString());
            ComboBoxImpact.SelectedIndex = 0;

            ComboBoxUrgency.Text = ticket.Urgency.ToString();

             DateSelectDeadline.SelectedDate = ticket.DeadLine;

            TextBoxDescription.Text = ticket.Description;


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
                   + ticket.Description.ToString() + "\n"
                   + ticket.DateReported.ToString() + "\n"
                   + ticket.DeadLine.ToString() + "\n";
            }
            MessageBox.Show(result);
        }

       

        private void ButtonTestDelete_Click(object sender, RoutedEventArgs e)
        {
            try {
                Ticket tickets = ticketLogic.ReadTicket()[0];
              MessageBox.Show(ticketLogic.DeleteTicket(tickets).ToString()); 
            } 
            catch (Exception ex)
            {

            }
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

            //test
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


            //test
            Ticket update = new Ticket(){
                DateReported = DateTime.Today,
                Subject = "update",
                Incident = IncidentType.hardware,
                User = new User(
                        new BsonKeyValuePair("id", ObjectId.GenerateNewId()),
                        new BsonKeyValuePair("userName", "Reynard96Blazer"),
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
                Description = "update"
            };


            try
            {
                MessageBox.Show(ticketLogic.UpdateTicket(ticket, update).ToString());
            }
            
            catch(Exception ex) { }
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void ButtonSubmit_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                ticketLogic.CreateTicket(MakeTicket());
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        public Ticket MakeTicket()
        {

            Ticket ticket = new Ticket()
            {
                DateReported = (DateTime)DatePickerDateTime.SelectedDate,

                Subject = TextBoxDescription.Text.ToString(),

                Incident = (IncidentType)ComboBoxIncidentType.SelectedIndex,

                User = (User)ComboBoxUser.SelectedValue,

                Impact = (Priority)ComboBoxImpact.SelectedIndex,

                Urgency = (Priority)ComboBoxUrgency.SelectedIndex,

                DeadLine = (DateTime)DateSelectDeadline.SelectedDate,

                Status = currentStatus

           };
            return ticket;
        }
    }
}
