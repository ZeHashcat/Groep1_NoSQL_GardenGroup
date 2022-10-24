namespace GardenGroupLogic
{
    public class DashboardControllerInstance
    {
        private static DashboardControllerInstance instance = null;
        private static readonly object padlock = new object();
        DashboardController dashboardController;

        DashboardControllerInstance()
        {
            dashboardController = new DashboardController();
        }

        public DashboardController DashboardController { get { return dashboardController; } }

        public static DashboardControllerInstance Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new DashboardControllerInstance();
                    }
                    return instance;
                }
            }
        }
    }
}
