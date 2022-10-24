using System.Windows;
using System.Windows.Controls;

namespace GardenGroupUI
{
    /// <summary>
    /// Interaction logic for DashboardToolbarUserControl.xaml
    /// </summary>
    public partial class DashboardToolbarUserControl : UserControl
    {
        public DashboardToolbarUserControl()
        {
            InitializeComponent();
        }

        private void buttonAddWidget_Click(object sender, RoutedEventArgs e)
        {
            AppMenuWindow widgetMenu = new AppMenuWindow("../Dashboard/DashboardMenuAddWidgetPage.xaml");
            widgetMenu.Show();
        }
    }
}
