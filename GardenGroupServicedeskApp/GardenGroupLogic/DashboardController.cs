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
        void AddWidget(IDashboardUserControl widget)
        {
            dashboardInstance.Dashboard.AddWidget(widget);
        }
        void RemoveWidget(IDashboardUserControl widget)
        {

        }
    }
}
