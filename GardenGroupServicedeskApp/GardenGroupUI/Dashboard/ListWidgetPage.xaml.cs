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
using GardenGroupLogic;

namespace GardenGroupUI
{
    /// <summary>
    /// Interaction logic for ListWidgetPage.xaml
    /// </summary>
    public partial class ListWidgetPage : Page, IWidgetObserver
    {
        IWidget widget;
        public ListWidgetPage(IWidget widget)
        {
            InitializeComponent();

            this.widget = widget;
            this.widget.AddObserver(this);
        }

        private void widgetList_Loaded(object sender, RoutedEventArgs e)
        {
            widgetList.BindingGroup = widget.GetData();
        }

        public void Update(IWidget widget)
        {
            this.widget = widget;
        }

        

        //List widget will load a list, it has several columns each with a header.

        //Type of lists:
        //User:
        //Team list(Checks Team, lists all users with Firstname or Lastname, Role, Amount of tickets assigned to user, ?tickets closed today?)
        //User list(Checks User, lists all tickets with status, ticket author and time since creation.)
        //Ticket:
        //Unassigned ticket list(Checks Ticket, lists all unassigned tickets, time since creation, ticket author)
        //Assigned ticket list(Checks Ticket, lists all assigned tickets, time since creation, ticket author, assigned user, team)
        //Closed tickets list(Checks Ticket, Lists all closed tickets, status(resolved/unresolved), datetime of closure, ticket author, user that closed ticket, team)
    }
}
