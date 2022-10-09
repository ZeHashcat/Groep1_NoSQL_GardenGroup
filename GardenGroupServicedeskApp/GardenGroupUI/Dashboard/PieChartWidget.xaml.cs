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
    /// Interaction logic for PieChartWidget.xaml
    /// </summary>
    public partial class PieChartWidget : Page
    {
        //Pie Chart has multiple counts, every count will be assigned a colour and will be listed alongside pie chart.
        private Dictionary<string, int> counts;
        //private var pieChart


        public PieChartWidget(Dictionary<string, int> counts)
        {
            this.counts = counts;
            
            InitializeComponent();

            InitLegend(this.counts);
            InitPieChart(this.counts);
        }

        private void InitPieChart(Dictionary<string, int> counts)
        {
            foreach(KeyValuePair<string, int> keyValuePair in counts)
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
