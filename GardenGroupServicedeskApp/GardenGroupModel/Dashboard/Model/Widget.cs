using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GardenGroupModel.Dashboard.Model
{
    public class Widget : IWidget
    {
        IWidgetData widgetData;

        private List<IWidgetObserver> widgetObservers;

        public Widget(IWidgetData widgetData)
        {
            this.widgetData = widgetData;
            this.widgetObservers = new List<IWidgetObserver>();
        }

        public void AddObserver(IWidgetObserver observer)
        {
            this.widgetObservers.Add(observer);
        }
        public void RemoveObserver(IWidgetObserver observer)
        {
            this.widgetObservers.Remove(observer);
        }

        public IWidgetData GetData()
        {
            return widgetData;
        }
    }
}
