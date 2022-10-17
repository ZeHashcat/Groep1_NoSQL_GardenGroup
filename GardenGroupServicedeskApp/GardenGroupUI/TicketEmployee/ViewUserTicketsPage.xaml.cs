using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
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
using GardenGroupLogic;
using GardenGroupModel;

namespace GardenGroupUI.TicketEmployee
{
    /// <summary>
    /// Interaction logic for ViewUserTicketsPage.xaml
    /// </summary>
    public partial class ViewUserTicketsPage : Page
    {
        TicketLogic ticketLogic = new TicketLogic();

        public ViewUserTicketsPage()
        {
            InitializeComponent();

            TicketFetcher();

            GridTicketOverview.MouseLeftButtonDown += (sen, evg) =>
            {
                //AppWindow.GetWindow(this).Content = new CRUDPage(CRUDState.Read);
                TicketWindow ticketWindow = new TicketWindow(CRUDState.Read);
                ticketWindow.Show();
                AppWindow.GetWindow(this).IsEnabled = false;
            };
        }

        public void TicketFetcher()
        {
            //fetch ticketarray from method in logic layer
            List<Ticket> tickets = ticketLogic.ReadTicket();

            /*tickets.Add(new Ticket()
            {
                DateReported = DateTime.Today,
                Subject = "test1",
                Incident = IncidentType.service,
                User = new User(null, null, null, null, null, null, null, null, null, null),
                Impact = Priority.high,
                Urgency = Priority.low,
                DeadLine = DateTime.Now,
                Description = "this is a test."
            });
            tickets.Add(new Ticket()
            {
                DateReported = DateTime.Today,
                Subject = "test2",
                Incident = IncidentType.service,
                User = new User(null, null, null, null, null, null, null, null, null, null),
                Impact = Priority.high,
                Urgency = Priority.low,
                DeadLine = DateTime.Now,
                Description = "this is a test."
            });
            tickets.Add(new Ticket()
            {
                DateReported = DateTime.Today,
                Subject = "test3",
                Incident = IncidentType.service,
                User = new User(null, null, null, null, null, null, null, null, null, null),
                Impact = Priority.high,
                Urgency = Priority.low,
                DeadLine = DateTime.Now,
                Description = "this is a test."
            });*/

            PrintTickets(tickets);
        }

        public void PrintTickets(List<Ticket> tickets)
        {
            for (int i = 0; i < tickets.Count; i++)
            {
                RowDefinition rowDef = new RowDefinition();
                rowDef.Height = new GridLength(40);
                GridTicketOverview.RowDefinitions.Add(rowDef);

                FillColumns(i, tickets);
            }
        }

        public void FillColumns(int i, List<Ticket> tickets)
        {
            for (int j = 0; j < GridTicketOverview.ColumnDefinitions.Count; j++)
            {
                TextBlock textBlock = new TextBlock();
                SelectTicketField(i, j, textBlock, tickets);
                FillTextBlockDetails(textBlock);
                Grid.SetRow(textBlock, i + 1);
                Grid.SetColumn(textBlock, j);
                GridTicketOverview.Children.Add(textBlock);

                SetBorders(i, j);
            }
        }

        public void SelectTicketField(int i, int j, TextBlock textBlock, List<Ticket> tickets)
        {
            switch (j)
            {
                case 0:
                    textBlock.Text = tickets[i].DeadLine.ToString();
                    break;
                case 1:
                    textBlock.Text = tickets[i].Subject.ToString();
                    break;
                case 2:
                    textBlock.Text = tickets[i].User.ToString();
                    break;
                case 3:
                    textBlock.Text = tickets[i].Description.ToString();
                    break;
                case 4:
                    textBlock.Text = tickets[i].DateReported.ToString();
                    break;
                case 5:
                    textBlock.Text = tickets[i].Status.ToString();
                    break;
            }
        }

        public void SetBorders(int row, int column)
        {
            Border border = new Border();
            if (column < GridTicketOverview.ColumnDefinitions.Count - 1)
            {
                border.BorderThickness = new Thickness(0, 1, 1, 0);
            }
            else
            {
                border.BorderThickness = new Thickness(0, 1, 0, 0);
            }
            border.BorderBrush = Brushes.Black;
            Grid.SetRow(border, row + 1);
            Grid.SetColumn(border, column);
            GridTicketOverview.Children.Add(border);
        }

        public TextBlock FillTextBlockDetails(TextBlock textBlock)
        {
            textBlock.HorizontalAlignment = HorizontalAlignment.Left;
            textBlock.VerticalAlignment = VerticalAlignment.Top;
            textBlock.Margin = new Thickness(5, 0, 20, 0);

            return textBlock;
        }


    }
}
