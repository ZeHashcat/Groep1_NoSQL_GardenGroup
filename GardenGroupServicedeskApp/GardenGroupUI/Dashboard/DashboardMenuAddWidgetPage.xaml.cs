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
using GardenGroupLogic;
using GardenGroupModel;

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
            WidgetType widgetType = (WidgetType)Enum.Parse(typeof(WidgetType), comboBoxWidgetType.SelectedItem.ToString());
            //
            //dashboardControllerInstance.DashboardController.AddWidget(widgetType, widgetContent);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            this.dashboardControllerInstance = DashboardControllerInstance.Instance;
            comboBoxWidgetType.ItemsSource = Enum.GetValues(typeof(WidgetType)).Cast<WidgetType>();
        }
    }
}
