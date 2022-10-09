using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using GardenGroupModel;

namespace GardenGroupUI
{
    internal static class ListWidget
    {
        public static ListView GetListView(ListView listView, List<ICollectionObject> list, string[,] columnHeaders)
        {
            GridView gridView = new GridView();
            for (int x = 0; x < columnHeaders.GetLength(0); x++)
            {
                gridView.Columns.Add(new GridViewColumn
                {
                    DisplayMemberBinding = new Binding(columnHeaders[x, 1]),
                    Header = columnHeaders[x, 0],
                    Width = double.NaN
                });
            }

            listView.View = gridView;

            foreach (ICollectionObject item in list)
            {
                listView.Items.Add(item);
            }

            return listView;
        }
    }
}
