﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GardenGroupModel
{
    public class Dashboard : IDashboard
    {
        //NOTE: Members here ↓
        private List<IDashboardUserControl> widgetList;

        //NOTE: Lists observers here ↓ 
        private List<IWidgetListObserver> observerList;

        //NOTE: Properties here ↓
        public List<IDashboardUserControl> WidgetList { get { return widgetList; } }

        //NOTE: Constructor here ↓, initialise observer lists
        public Dashboard()
        {
            widgetList = new List<IDashboardUserControl>();
            observerList = new List<IWidgetListObserver>();
        }

        //NOTE: Methods here ↓
        public void AddWidget(IDashboardUserControl widget)
        {
            widgetList.Add(widget);
        }

        public void RemoveWidget(IDashboardUserControl widget)
        {
            widgetList.Remove(widget);
        }

        //NOTE: Add/Remove/Notify observers here ↓
        public void AddObserver(IWidgetListObserver observer)
        {
            observerList.Add(observer);
        }
        public void RemoveObserver(IWidgetListObserver observer)
        {
            observerList.Remove(observer);
        }
    }
}