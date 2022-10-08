using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace GardenGroupModel
{
    public interface IDashboardController
    {
        //Methods down here ↓
        public void AddWidget(WidgetType widgetType, string widgetContent);
        public void RemoveWidget(IDashboardUserControl widget);


    }
}
