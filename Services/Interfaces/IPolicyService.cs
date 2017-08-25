using MDW.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MDW.Services.Interfaces
{
    public interface IPolicyService
    {
        Task<bool> Evaluate(string username, string url);

        Task<List<Policy>> GetPolicies();

        Task<bool> CreatePolicy(string role, string group, bool effect);

        Task<bool> DeletePolicy(string role, string group);
    }
}