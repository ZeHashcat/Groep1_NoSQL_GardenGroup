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
        public void AddWidget(IWidget widget);
        public void RemoveWidget(IWidget widget);


    }
}
