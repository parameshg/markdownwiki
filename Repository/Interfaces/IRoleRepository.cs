using MDW.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MDW.Repository.Interfaces
{
    public interface IRoleRepository
    {
        Task<List<Role>> GetRoles();

        Task<Role> GetRole(string role);

        Task<bool> CreateRole(Role role);

        Task<bool> DeleteRole(string role);
    }
}