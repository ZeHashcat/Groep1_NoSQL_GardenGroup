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
using MongoDB.Driver;
using System.Reflection;

namespace GardenGroupUI
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class AppMainPage : Page
    {
        AppWindow appWindow = (AppWindow)Application.Current.MainWindow;
        
        public AppMainPage()
        {
                InitializeComponent();
        }
    }
}
