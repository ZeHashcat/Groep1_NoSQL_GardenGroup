using GardenGroupLogic;
using GardenGroupModel;
using System;
using System.Windows;
using System.Windows.Controls;

namespace GardenGroupUI
{
    /// <summary>
    /// Interaction logic for WidgetPage.xaml
    /// </summary>
    public partial class DashboardWidgetPage : Page, IWidget
    {
        private DashboardControllerInstance dashboardControllerInstance;
        int index; //To change order of widgets.

        public DashboardWidgetPage()
        {
            InitializeComponent();
            WidgetContent.Source = new Uri("WidgetData/TeamUsersList.xaml", UriKind.Relative);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            this.dashboardControllerInstance = DashboardControllerInstance.Instance;
        }

        private void buttonCloseWidget_Click(object sender, RoutedEventArgs e)
        {
            dashboardControllerInstance.DashboardController.RemoveWidget(this);
        }
    }
}
