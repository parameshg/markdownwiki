using MDW.Entity;
using MDW.Repository.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MDW.Repository
{
    public class RoleRepository : RepositoryBase, IRoleRepository
    {
        public async Task<List<Role>> GetRoles()
        {
            var result = new List<Role>();

            await Task.Run(() =>
            {
                result.AddRange(Database.GetCollection<Role>().FindAll());
            });

            return result;
        }

        public async Task<Role> GetRole(string role)
        {
            Role result = null;

            await Task.Run(() =>
            {
                result = Database.GetCollection<Role>().FindOne(i => i.Name.Equals(role));
            });

            return result;
        }

        public async Task<bool> CreateRole(Role role)
        {
            var result = false;

            await Task.Run(() =>
            {
                result = Database.GetCollection<Role>().Upsert(role.Name, role);
            });

            return result;
        }

        public async Task<bool> DeleteRole(string role)
        {
            var result = false;

            await Task.Run(() =>
            {
                result = Database.GetCollection<Role>().Delete(i => i.Name.Equals(role)) > 0;
            });

            return result;
        }
    }
}