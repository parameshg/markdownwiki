using MDW.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MDW.Services.Interfaces
{
    public interface IGroupService
    {
        Task<List<Group>> GetGroups();

        Task<Group> GetGroup(string name);

        Task<bool> CreateGroup(string name, bool builtin = false);

        Task<bool> DeleteGroup(string name);
    }
}