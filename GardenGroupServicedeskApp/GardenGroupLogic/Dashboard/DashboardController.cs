using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GardenGroupModel;

namespace GardenGroupLogic
{
    public class DashboardController : IDashboardController
    {
        private DashboardInstance dashboardInstance;

        public DashboardController()
        {
            this.dashboardInstance = DashboardInstance.Instance;
        }

        //this.dashboard methods without params here ↓

        //this.dashboard methods with params down here ↓
        public void AddWidget(WidgetType widgetType, string widgetContent)
        {
            //CODE: create widget depending on string widgetType and string widgetContent.
            //dashboardInstance.Dashboard.AddWidget(widget);
        }
        public void RemoveWidget(IDashboardUserControl widget)
        {
            dashboardInstance.Dashboard.RemoveWidget(widget);
        }
    }
}
