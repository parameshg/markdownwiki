using MDW.Entity;
using MDW.Repository.Interfaces;
using MDW.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace MDW.Services
{
    public class PolicyService : ServiceBase, IPolicyService
    {
        private IUserRepository Users { get; set; }

        private IPageRepository Pages { get; set; }

        private IImageRepository Images { get; set; }

        private IPolicyRepository Policies { get; set; }

        public PolicyService(IUserRepository users, IPageRepository pages, IImageRepository images, IPolicyRepository policies)
        {
            Users = users;
            Pages = pages;
            Images = images;
            Policies = policies;
        }

        public async Task<bool> EvaluatePage(string username, string url)
        {
            var result = false;

            var user = await Users.GetUser(username);

            var role = user != null ? user.Role : string.Empty;

            var page = await Pages.GetPageByUrl(url);

            var group = page != null ? page.Group : string.Empty;

            if (!string.IsNullOrEmpty(role) && !string.IsNullOrEmpty(group))
            {
                var policies = await Policies.GetPolicies();

                var flag = true;

                foreach(var policy in policies.Where(i => i.Role.Equals(role) && i.Group.Equals(group)).ToList())
                    flag = flag && policy.Effect;

                result = flag;
            }

            return result;
        }

        public async Task<bool> EvaluateImage(string username, string filename)
        {
            var result = false;

            var user = await Users.GetUser(username);

            var role = user != null ? user.Role : string.Empty;

            var image = await Images.GetImage(filename);

            var group = image != null ? image.Group : string.Empty;

            if (!string.IsNullOrEmpty(role) && !string.IsNullOrEmpty(group))
            {
                var policies = await Policies.GetPolicies();

                var flag = true;

                foreach (var policy in policies.Where(i => i.Role.Equals(role) && i.Group.Equals(group)).ToList())
                    flag = flag && policy.Effect;

                result = flag;
            }

            return result;
        }

        public async Task<List<Policy>> GetPolicies()
        {
            var result = new List<Policy>();

            result.AddRange(await Policies.GetPolicies());

            return result;
        }

        public async Task<bool> CreatePolicy(string role, string group, bool effect)
        {
            var result = false;

            result = await Policies.CreatePolicy(role, group);

            return result;
        }

        public async Task<bool> DeletePolicy(string role, string group)
        {
            var result = false;

            result = await Policies.DeletePolicy(role, group);

            return result;
        }
    }
}