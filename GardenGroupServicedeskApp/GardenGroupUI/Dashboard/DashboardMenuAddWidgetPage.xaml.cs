using GardenGroupLogic;
using GardenGroupModel;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace GardenGroupUI
{
    /// <summary>
    /// Interaction logic for AppMenuAddWidgetPage.xaml
    /// </summary>
    public partial class DashboardMenuAddWidgetPage : Page
    {
        private DashboardControllerInstance dashboardControllerInstance;

        public DashboardMenuAddWidgetPage()
        {
            InitializeComponent();
        }

        private void buttonAddWidgetAdd_Click(object sender, RoutedEventArgs e)
        {
            //WidgetType widgetType = (WidgetType)Enum.Parse(typeof(WidgetType), comboBoxWidgetType.SelectedItem.ToString());

            //TEST:
            string source = "WidgetData/TeamUsersList.xaml";
            IWidget widget = new DashboardWidgetPage();
            dashboardControllerInstance.DashboardController.AddWidget(widget);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            this.dashboardControllerInstance = DashboardControllerInstance.Instance;
            comboBoxWidgetClass.ItemsSource = Enum.GetValues(typeof(WidgetClass)).Cast<WidgetClass>();
            comboBoxWidgetType.ItemsSource = Enum.GetValues(typeof(WidgetType)).Cast<WidgetType>();
        }
    }
}
