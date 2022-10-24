using GardenGroupDAL;

namespace GardenGroupLogic
{
    public class RoleService
    {
        private RoleDAO roleDAO;
        public RoleService()
        {
            roleDAO = new RoleDAO();
        }

        public async Task<List<string>> GetRoles()
        {
            List<string> roles = await Task.Run(() => roleDAO.GetRoles());
            return roles;

        }
    }
}
