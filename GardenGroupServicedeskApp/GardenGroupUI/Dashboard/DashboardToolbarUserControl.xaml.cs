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

namespace GardenGroupUI
{
    /// <summary>
    /// Interaction logic for DashboardToolbarUserControl.xaml
    /// </summary>
    public partial class DashboardToolbarUserControl : UserControl, IDashboardUserControl
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