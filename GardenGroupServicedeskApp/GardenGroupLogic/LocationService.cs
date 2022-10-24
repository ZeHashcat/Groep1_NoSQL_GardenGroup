using GardenGroupDAL;

namespace GardenGroupLogic
{
    public class LocationService
    {
        private LocationDAO locationDAO;
        public LocationService()
        {
            locationDAO = new LocationDAO();
        }

        public async Task<List<string>> GetLocations()
        {
            List<string> locations = await Task.Run(() => locationDAO.GetLocations());
            return locations;

        }
    }
}
