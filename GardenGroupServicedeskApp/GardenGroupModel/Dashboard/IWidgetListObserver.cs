﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GardenGroupModel
{
    public interface IWidgetListObserver
    {
        void Update(List<IDashboardUserControl> list);
    }
}