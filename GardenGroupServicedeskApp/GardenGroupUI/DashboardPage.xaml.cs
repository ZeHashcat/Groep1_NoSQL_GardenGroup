﻿using System;
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
    /// Interaction logic for DashboardPage.xaml
    /// </summary>
    public partial class DashboardPage : Page, IWidgetListObserver
    {
        private DashboardInstance dashboardInstance;

        public DashboardPage()
        {
            InitializeComponent();

            this.dashboardInstance = DashboardInstance.Instance;

            //Add this as observer to dashboard
            this.dashboardInstance.Dashboard.AddObserver(this);
        }

        //update methods from dashboard here ↓

        public void Update(List<IDashboardUserControl> widgetList)
        {
            //Refresh page and load widgets from widget list
        }

        //Any page events here ↓
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            //Load widgets from widget list
        }

        
    }
}
