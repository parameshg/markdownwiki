using MDW.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MDW.Repository.Interfaces
{
    public interface IPolicyRepository
    {
        Task<List<Policy>> GetPolicies();

        Task<bool> CreatePolicy(string role, string group);

        Task<bool> DeletePolicy(string role, string group);
    }
}