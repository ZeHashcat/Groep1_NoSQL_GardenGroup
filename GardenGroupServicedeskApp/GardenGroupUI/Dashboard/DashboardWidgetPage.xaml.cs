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
using System.Windows.Navigation;
using System.Windows.Shapes;
using GardenGroupModel;
using GardenGroupLogic;

namespace GardenGroupUI
{
    /// <summary>
    /// Interaction logic for WidgetPage.xaml
    /// </summary>
    public partial class DashboardWidgetPage : Page, IWidget
    {
        private DashboardControllerInstance dashboardControllerInstance;

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
