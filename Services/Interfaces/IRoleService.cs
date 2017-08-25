using MDW.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MDW.Services.Interfaces
{
    public interface IRoleService
    {
        Task<List<Role>> GetRoles();

        Task<Role> GetRole(string name);

        Task<bool> CreateRole(string name, bool builtin = false);

        Task<bool> DeleteRole(string name);
    }
}