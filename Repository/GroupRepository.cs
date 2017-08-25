using MDW.Entity;
using MDW.Repository.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MDW.Repository
{
    public class GroupRepository : RepositoryBase, IGroupRepository
    {
        public async Task<List<Group>> GetGroups()
        {
            var result = new List<Group>();

            await Task.Run(() =>
            {
                result.AddRange(Database.GetCollection<Group>().FindAll());
            });

            return result;
        }

        public async Task<Group> GetGroup(string group)
        {
            Group result = null;

            await Task.Run(() =>
            {
                result = Database.GetCollection<Group>().FindOne(i => i.Name.Equals(group));
            });

            return result;
        }

        public async Task<bool> CreateGroup(Group group)
        {
            var result = false;

            await Task.Run(() =>
            {
                result = Database.GetCollection<Group>().Upsert(group.Name, group);
            });

            return result;
        }

        public async Task<bool> DeleteGroup(string group)
        {
            var result = false;

            await Task.Run(() =>
            {
                result = Database.GetCollection<Group>().Delete(i => i.Name.Equals(group)) > 0;
            });

            return result;
        }
    }
}