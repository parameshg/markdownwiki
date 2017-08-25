using MDW.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MDW.Repository.Interfaces
{
    public interface IGroupRepository
    {
        Task<List<Group>> GetGroups();

        Task<Group> GetGroup(string group);

        Task<bool> CreateGroup(Group group);

        Task<bool> DeleteGroup(string group);
    }
}