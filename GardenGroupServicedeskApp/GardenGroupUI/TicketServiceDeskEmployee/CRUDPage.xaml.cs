﻿using System.Windows.Controls;
using System.Windows;
using System;
using GardenGroupModel;
using System.Collections.Generic;
using GardenGroupLogic;
using System.Xml.Linq;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Drawing;
using System.Windows.Media;

namespace GardenGroupUI
{
    
    public partial class CRUDPage : Page
    {

        TicketLogic ticketLogic = new TicketLogic();

        public CRUDPage(CRUDState state)
        {
            Priority priority = new Priority();

            //sets a ticket to test whit
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

            ComboBoxUser.Items.Add(user.User.FirstName.ToString());
            DateSelectDeadline.SelectedDate= DateTime.Today.AddDays(7);
        }

        public void UpdateSetup()
        {


            PreFillForm(this.ticket);

            //update button
            ButtonCancel.Content = "update";
            ButtonCancel.Click -= new RoutedEventHandler(ButtonCancel_Click);
            ButtonCancel.Click += new RoutedEventHandler(UpdateTicket);

            //delete button
            ButtonSubmit.Content = "delete";
            ButtonSubmit.Background = new SolidColorBrush(Colors.Red);
            ButtonSubmit.Click -= new RoutedEventHandler(ButtonSubmit_Click);
            ButtonSubmit.Click += new RoutedEventHandler(DeleteTicket);

        }
        public void ReadSetup()
        {
            this.IsEnabled = false;
            
             DatePickerDateTime.SelectedDate = ticket.DateReported;
            

            //ticketLogic.CreateTicket(tickets[0]);
            foreach (Ticket ticket in tickets) { 
             result += ticket.Subject.ToString() + "\n"
                + ticket.Description.ToString();
            }
            MessageBox.Show(result);


             ComboBoxIncidentType.Items.Add (ticket.Incident);
            ComboBoxIncidentType.SelectedIndex = 0;

             ComboBoxUser.Items.Add(ticket.User.FirstName.Value.ToString());
            ComboBoxUser.SelectedIndex = 0;



            ComboBoxImpact.Items.Add(ticket.Impact.ToString());
            ComboBoxImpact.SelectedIndex = 0;

            ComboBoxUrgency.Items.Add(ticket.Urgency.ToString());
            ComboBoxUrgency.SelectedIndex = 0;


            DateSelectDeadline.SelectedDate = ticket.DeadLine;

            TextBoxDescription.Text = ticket.Description;

            buttonGroup.Children.Remove(ButtonCancel);
            buttonGroup.Children.Remove(ButtonSubmit);

        }

        private void SetupUsers(List<User> users)
        {
           // ComboBoxUser.SetValue(users.);
        }
        private void SetupUsers(User user)
        {

        }

        //test methods
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

       
        //button events
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

        private void UpdateTicket(object sender, RoutedEventArgs e)
        {
            ticketLogic.UpdateTicket(this.ticket,MakeTicket());
        }
        private void DeleteTicket(object sender, RoutedEventArgs e)
        {

        }
        //help methods
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

        public void PreFillForm(Ticket ticket)
        {


            DatePickerDateTime.SelectedDate = ticket.DateReported;


            TextBoxSubject.Text = ticket.Subject;


            ComboBoxIncidentType.Items.Add(ticket.Incident);
            ComboBoxIncidentType.SelectedIndex = 0;

            ComboBoxUser.Items.Add(ticket.User.FirstName.Value.ToString());
            ComboBoxUser.SelectedIndex = 0;



            ComboBoxImpact.Items.Add(ticket.Impact.ToString());
            ComboBoxImpact.SelectedIndex = 0;

            ComboBoxUrgency.Items.Add(ticket.Urgency.ToString());
            ComboBoxUrgency.SelectedIndex = 0;


            DateSelectDeadline.SelectedDate = ticket.DeadLine;

            TextBoxDescription.Text = ticket.Description;

          
        }
    }
}
