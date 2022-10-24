namespace GardenGroupModel
{
    public class DashboardInstance
    {
        private static DashboardInstance instance = null;
        private static readonly object padlock = new object();
        private Dashboard dashboard;

        DashboardInstance()
        {
            dashboard = new Dashboard();
        }

        public Dashboard Dashboard { get { return dashboard; } }

        public static DashboardInstance Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new DashboardInstance();
                    }
                    return instance;
                }
            }
        }
    }
}
