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

namespace GardenGroupUI
{
    /// <summary>
    /// Interaction logic for AppMenuWindow.xaml
    /// </summary>
    public partial class AppMenuWindow : Window
    {
        public AppMenuWindow(string menuToOpen)
        {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            frameContent.Source = new Uri(menuToOpen, UriKind.Relative);
        }
    }
}
