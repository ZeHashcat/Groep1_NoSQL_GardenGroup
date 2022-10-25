using GardenGroupLogic;
using GardenGroupModel;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace GardenGroupUI.TicketEmployee
{
    /// <summary>
    /// Interaction logic for TicketPage.xaml
    /// </summary>
    public partial class TicketWindow : Window
    {
        TicketLogic ticketLogic = new TicketLogic();

        Page ticketOverviewPage;
        Ticket ?ticket;
        private UserInstance user = UserInstance.GetUserInstance();
        UserLogic UserLogic = new UserLogic();

        public TicketWindow(CRUDState state, Page page, Ticket? ticket)
        {
            this.ticketOverviewPage = page;
            this.ticket = ticket;


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
                    DeleteSetup();
                    break;
            }
        }

        public void CreateSetup()
        {
            //fill drop dowms
            //date time selector fill
            DatePickerDateTime.SelectedDate = DateTime.Today;
            //fill dropdown whit users
            //ComboBoxUser.Items.Add(user.User.UserName.Value.ToString());
            
            foreach (User user in UserLogic.GetAllusers())
            {
                ComboBoxUser.Items.Add(user.UserName.Value.ToString());
            }

            ComboBoxUser.SelectedItem = user.User.UserName.Value.ToString();

            if (user.User.Role.Value=="Admin")
            {
                ComboBoxUser.IsEnabled = true;
            }
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

            DateSelectDeadline.SelectedDate = DateTime.Today.AddDays(7);
            
        }
        public void CreateSetupPriviledged()
        {
            CreateSetup();
            ComboBoxUser.IsEnabled= false;

        }

        public void UpdateSetup()
        {
            CreateSetup();
            
            PreFillForm(this.ticket);

            //update button
            ButtonCancel.Content = "Update";
            ButtonCancel.Click -= new RoutedEventHandler(ButtonCancel_Click);
            ButtonCancel.Click += new RoutedEventHandler(UpdateTicket);
            ButtonCancel.Background = Brushes.Yellow;

            //delete button
            ButtonSubmit.Content = "Delete";
            ButtonSubmit.Background = new SolidColorBrush(Colors.Red);
            ButtonSubmit.Click -= new RoutedEventHandler(ButtonSubmit_Click);
            ButtonSubmit.Click += new RoutedEventHandler(DeleteTicket);
            //cancel button
            Button cancelbutton = new Button();
            cancelbutton.Content = "Cancel";
            cancelbutton.Click += new RoutedEventHandler(ButtonCancel_Click);

            //resolve
            Button resolvebutton = new Button();
            resolvebutton.Content = "Resolve";
            resolvebutton.Click += new RoutedEventHandler(ButtonResolve);
            resolvebutton.Background = new SolidColorBrush(Colors.Green);

            //grid
            Grid grid = buttonGroup;
            grid.Children.Add(cancelbutton);
            Grid.SetRow(cancelbutton, 1);
            Grid.SetColumn(cancelbutton, 0);


            grid.Children.Add(resolvebutton);
            Grid.SetRow(resolvebutton, 1);
            Grid.SetColumn(resolvebutton, 1);


            //update button in grid
            Grid.SetRow(ButtonCancel, 0);
            Grid.SetColumn(ButtonCancel, 1);
            //delete button in grid
            Grid.SetRow(ButtonSubmit, 0);
            Grid.SetColumn(ButtonSubmit, 0);





        }
        public void ReadSetup()
        {
            CreateSetup();
            PreFillForm(this.ticket);


            buttonGroup.Children.Remove(ButtonSubmit);
            disableFields();

        }
        public void DeleteSetup()
        {

            PreFillForm(this.ticket);




            ButtonSubmit.Content = "delete";
            ButtonSubmit.Background = new SolidColorBrush(Colors.Red);
            ButtonSubmit.Click -= new RoutedEventHandler(ButtonSubmit_Click);
            ButtonSubmit.Click += new RoutedEventHandler(DeleteTicket);

            ButtonSubmit.IsEnabled = true;
        }




   




        //button events
        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            AppMenuWindow.GetWindow(ticketOverviewPage).IsEnabled = true;
            AppMenuWindow.GetWindow(ticketOverviewPage).Focus();

            AppMenuWindow.GetWindow(this).Close();

        }
        private void ButtonSubmit_Click(object sender, RoutedEventArgs e)
        {

            try
            {

                ticketLogic.CreateTicket(MakeTicket(UserLogic.GetUser(ComboBoxUser.SelectedItem.ToString())));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void ButtonResolve(object sender, RoutedEventArgs e)
        {

            try
            {
                Ticket ticket =  MakeTicket(user.User);
                ticket.Status = TicketStatus.resolved;
                ticketLogic.CreateTicket(ticket);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void UpdateTicket(object sender, RoutedEventArgs e)
        {
            UserLogic userLogic = new UserLogic();
            //User user = userLogic.GetUser(ComboBoxUser.SelectedValue.ToString());
            User user = userLogic.GetUser(ComboBoxUser.Text);

            ticketLogic.UpdateTicket(this.ticket, MakeTicket(user, ticket));
        }
        private void DeleteTicket(object sender, RoutedEventArgs e)
        {
            ticketLogic.DeleteTicket(ticket);
        }
        //help methods
        //make tickets
        public Ticket MakeTicket(User user)
        {

            if (!ValidateFields())
            {
                throw new Exception("not all fields are filled");
            }
          
         
            //ObjectId id = this.ticket._id;
            Ticket ticketToReturn = new Ticket()

            {
                _id = (ObjectId)new BsonObjectId(ObjectId.GenerateNewId()),

                DateReported = (DateTime)DatePickerDateTime.SelectedDate,

                Subject = TextBoxSubject.Text.ToString(),

                Description = TextBoxDescription.Text.ToString(),

                Incident = (IncidentType)ComboBoxIncidentType.SelectedIndex,

                User = user,

                Impact = (Priority)ComboBoxImpact.SelectedIndex,

                Urgency = (Priority)ComboBoxUrgency.SelectedIndex,

                DeadLine = (DateTime)DateSelectDeadline.SelectedDate,

                Status = 0

            };

            return ticketToReturn;
        }
        private Ticket MakeTicket(User user, Ticket ticket)
        {


            Ticket ticketToReturn = new Ticket()
            {
                _id = ticket._id,

                DateReported = (DateTime)DatePickerDateTime.SelectedDate,

                Subject = TextBoxSubject.Text.ToString(),

                Description = TextBoxDescription.Text.ToString(),

                Incident = (IncidentType)ComboBoxIncidentType.SelectedIndex,

                User = user,

                Impact = (Priority)ComboBoxImpact.SelectedIndex,

                Urgency = (Priority)ComboBoxUrgency.SelectedIndex,

                DeadLine = (DateTime)DateSelectDeadline.SelectedDate,

                Status = (TicketStatus)ComboBoxIncidentType.SelectedIndex

            };
            return ticketToReturn;
        }
        /// <summary>
        /// validates the fields of the form if they are correctly filled in
        /// </summary>
        /// <returns></returns>
        private bool ValidateFields()
        {
            bool isValid = true;
            if (DatePickerDateTime.SelectedDate == null)
            {
                DatePickerDateTime.BorderBrush = Brushes.Red;
                isValid = false;
            }
            else
            {
                DatePickerDateTime.BorderBrush = Brushes.Black;

            }
            if (TextBoxSubject.Text == String.Empty)
            {
                TextBoxSubject.BorderBrush = Brushes.Red;
                SubjectIncidentTextBlock.Foreground = Brushes.Red;
                isValid = false;

            }
            else
            {
                TextBoxSubject.BorderBrush = Brushes.Black;
                SubjectIncidentTextBlock.Foreground = Brushes.Black;

            }
            if (ComboBoxIncidentType.SelectedItem == null)
            {
                ComboBoxIncidentType.Background = Brushes.Black;
                TypeOfIncidentTextBox.Foreground = Brushes.Red;
                isValid = false;

            }
            else
            {
                TypeOfIncidentTextBox.Foreground = Brushes.Black;

            }
            if (ComboBoxUser.SelectedItem == null)
            {
                ComboBoxUser.Foreground = Brushes.Black;
                ReportedTextBlock.Foreground = Brushes.Red;
                isValid = false;

            }
            else
            {
                ReportedTextBlock.Foreground = Brushes.Black;
            }
            if (ComboBoxImpact.SelectedItem == null)
            {
                ComboBoxImpact.Foreground = Brushes.Black;
                ImpactTextBlock.Foreground = Brushes.Red;
                isValid = false;

            }
            else
            {
                ImpactTextBlock.Foreground = Brushes.Black;
            }
            if (ComboBoxUrgency.SelectedItem == null)
            {
                ComboBoxUrgency.Foreground = Brushes.Black;
                UrgencyTextBlock.Foreground = Brushes.Red;
                isValid = false;

            }
            else
            {
                UrgencyTextBlock.Foreground = Brushes.Black;
            }
            if (DateSelectDeadline.SelectedDate == null)
            {
                DateSelectDeadline.Foreground = Brushes.Red;
                DeadlineTekstblock.Foreground = Brushes.Red;
                isValid = false;

            }
            else
            {
                DeadlineTekstblock.Foreground = Brushes.Black;
            }
            if (TextBoxDescription.Text == String.Empty)
            {
                TextBoxDescription.BorderBrush = Brushes.Red;
                DescriptionTekstBlock.Foreground = Brushes.Red;
                isValid = false;

            }
            else
            {
                DescriptionTekstBlock.Foreground = Brushes.Black;
                DescriptionTekstBlock.Foreground = Brushes.Black;

            }



            return isValid;
        }
        /// <summary>
        /// disables all fields to stop interaction
        /// </summary>
        private void disableFields()
        {
            DatePickerDateTime.IsEnabled = false;

            TextBoxSubject.IsEnabled = false;

            TextBoxDescription.IsEnabled = false;

            ComboBoxIncidentType.IsEnabled = false;

            ComboBoxUser.IsEnabled = false;

            ComboBoxImpact.IsEnabled = false;

            ComboBoxUrgency.IsEnabled = false;

            DateSelectDeadline.IsEnabled = false;

            ComboBoxIncidentType.IsEnabled = false;
        }
        /// <summary>
        /// prefills the form whit values from a ticket
        /// </summary>
        /// <param name="ticket"></param>
        private void PreFillForm(Ticket ticket)
        {


            DatePickerDateTime.SelectedDate = ticket.DateReported;


            TextBoxSubject.Text = ticket.Subject;
            for (int i = 0; i < ComboBoxIncidentType.Items.Count; i++)
            {
                if (ComboBoxIncidentType.Items[i].ToString() == ticket.Incident.ToString())
                {
                    ComboBoxIncidentType.SelectedIndex = i;
                }

            }


            //ComboBoxUser.Items.Add(ticket.User.UserName.Value.ToString());
           // ComboBoxUser.Items.IndexOf(ticket.User.UserName.Value.ToString());
            ComboBoxUser.SelectedItem = ticket.User.UserName.Value.ToString();



            ComboBoxImpact.SelectedItem = ticket.Impact.ToString();

            ComboBoxUrgency.SelectedItem = ticket.Urgency.ToString();


            DateSelectDeadline.SelectedDate = ticket.DeadLine;

            TextBoxDescription.Text = ticket.Description;
        }
    }
}