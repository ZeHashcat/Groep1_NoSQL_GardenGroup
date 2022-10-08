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

            this.dashboardControllerInstance = DashboardControllerInstance.Instance;
        }

        private void buttonAddWidgetAdd_Click(object sender, RoutedEventArgs e)
        {
            string widgetType = comboBoxWidgetType.SelectedItem.ToString();
            string widgetContent = comboBoxWidgetContent.SelectedItem.ToString();
            dashboardControllerInstance.DashboardController.AddWidget(widgetType, widgetContent)
        }
    }
}
