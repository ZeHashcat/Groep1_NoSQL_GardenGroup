using System;
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
using GardenGroupModel;

namespace GardenGroupUI.Dashboard
{
    /// <summary>
    /// Interaction logic for GraphWidgetPage.xaml
    /// </summary>
    public partial class GraphWidgetPage : Page
    {
        //Graph has multiple counts, every count will be assigned a colour and will be listed alongside graph
        private Dictionary<string, int> counts;
        //private var Graph

        public GraphWidgetPage(Dictionary<string, int> counts)
        {
            this.counts = counts;

            InitializeComponent();

            InitLegend(this.counts);
            InitGraph(this.counts);
        }

        private void InitGraph(Dictionary<string, int> counts)
        {
            foreach (KeyValuePair<string, int> keyValuePair in counts)
            {
                //add keyValuePair to pieChart
            }
        }

        private void InitLegend(Dictionary<string, int> counts)
        {
            foreach (KeyValuePair<string, int> keyValuePair in counts)
            {
                //Add keyValuePair to legend
            }
        }

        //Type of Graph/PieChart
        //User:
        //MUST: Employee show my tickets %open %resolved %unresolved
        //CurrentUser show Resolved/Unresolved tickets
        //Team show open tickets per user
        //Teams show open tickets over all teams
        //Ticket:
        //MUST: show all tickets %open %resolved %unresolved
        //Show open ticket counts incidenttype
        //Show resolved ticket counts incidenttype
        //Show unresolved ticket counts incidenttype
    }
}
