using GardenGroupModel;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

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
            listView = SetListViewProperties(listView);
            return listView;
        }

        private static ListView SetListViewProperties(ListView listView)
        {
            listView.MinHeight = 250;
            listView.MinWidth = 300;
            listView.Margin = new Thickness(15);
            return listView;
        }
    }
}
