using MDW.Entity;
using MDW.Repository.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MDW.Repository
{
    public class PolicyRepository : RepositoryBase, IPolicyRepository
    {
        public async Task<List<Policy>> GetPolicies()
        {
            var result = new List<Policy>();

            await Task.Run(() =>
            {
                result.AddRange(Database.GetCollection<Policy>().FindAll());
            });

            return result;
        }

        public async Task<bool> CreatePolicy(string role, string group)
        {
            var result = false;

            await Task.Run(() =>
            {
                var policy = new Policy()
                {
                    Group = group,
                    Role = role,
                    Effect = true
                };

                var id = Database.GetCollection<Policy>().Insert(policy);

                result = id != null;
            });

            return result;
        }

        public async Task<bool> DeletePolicy(string role, string group)
        {
            var result = false;

            await Task.Run(() =>
            {
                result = Database.GetCollection<Policy>().Delete(i => i.Role.Equals(role) && i.Group.Equals(group)) > 0;
            });

            return result;
        }
    }
}