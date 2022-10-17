using GardenGroupLogic;
using GardenGroupModel;
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

namespace GardenGroupUI.TicketEmployee
{
    /// <summary>
    /// Interaction logic for TicketPage.xaml
    /// </summary>
    public partial class TicketWindow : Window
    {
        TicketLogic ticketLogic = new TicketLogic();

        public TicketWindow(CRUDState state)
        {
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

            DateSelectDeadline.SelectedDate = DateTime.Today.AddDays(7);
        }

        public void UpdateSetup()
        {

        }

        public void ReadSetup()
        {
            String result = "";
            List<Ticket> tickets = ticketLogic.ReadTicket();

            //ticketLogic.CreateTicket(tickets[0]);
            foreach (Ticket ticket in tickets)
            {
                result += ticket.Subject.ToString() + "\n"
                   + ticket.Description.ToString();
            }
            MessageBox.Show(result);
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
