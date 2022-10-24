﻿using GardenGroupModel;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace GardenGroupUI
{
    /// <summary>
    /// Interaction logic for DashboardPage.xaml
    /// </summary>

    public partial class DashboardServiceDeskEmployeePage : Page, IWidgetListObserver
    {
        private DashboardInstance dashboardInstance;
        public DashboardServiceDeskEmployeePage()
        {
            InitializeComponent();

            this.dashboardInstance = DashboardInstance.Instance;

            //Add this as observer to dashboard
            this.dashboardInstance.Dashboard.AddObserver(this);
        }

        //update methods from dashboard here ↓

        public void Update(List<IWidget> widgetList)
        {
            DashboardWrapPanel.Children.Clear();
            foreach (DashboardWidgetPage widget in widgetList)
            {
                Frame frame = new Frame();
                frame.Content = widget;
                DashboardWrapPanel.Children.Add(frame);
            }
        }

        //Any page events here ↓
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            //Load widgets from widget list
        }


    }
}
