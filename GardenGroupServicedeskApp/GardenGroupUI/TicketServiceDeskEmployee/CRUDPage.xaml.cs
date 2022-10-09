using System.Windows.Controls;
using System.Windows;
using System;
using GardenGroupModel;




namespace GardenGroupUI
{
    
    public partial class CRUDPage : Page
    {
        AppMainPage appMainPage = new AppMainPage();

        public CRUDPage(CRUDState state)
        {
            Priority priority = new Priority();
            InitializeComponent();
            Console.WriteLine(Application.Current.Properties.Values);
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
                ComboBoxPriority.Items.Add(Enum.GetName(typeof(IncidentType), i));
            }
        }

        public void UpdateSetup()
        {

        }
        public void ReadSetup()
        {

        }

        private void ButtonSubmit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
