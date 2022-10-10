using System.Windows.Controls;
using System.Windows;
using System;
using GardenGroupModel;
using System.Collections.Generic;
using GardenGroupLogic;

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
            ticketLogic.ReadTicket();

        }

        private void SetupUsers(List<User> users)
        {
           // ComboBoxUser.SetValue(users.);
        }
        private void SetupUsers(User user)
        {

        }

    }
}
