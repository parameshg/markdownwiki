using MDW.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MDW.Services.Interfaces
{
    public interface IPolicyService
    {
        Task<bool> EvaluatePage(string username, string url);

        Task<bool> EvaluateImage(string username, string filename);

        Task<List<Policy>> GetPolicies();

        Task<bool> CreatePolicy(string role, string group, bool effect);

        Task<bool> DeletePolicy(string role, string group);
    }
}