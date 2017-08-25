using System.Collections.Generic;

namespace MDW.Models
{
    public class PolicyListModel
    {
        public List<RoleModel> Roles { get; set; }

        public List<GroupModel> Groups { get; set; }

        public List<PolicyModel> Policies { get; set; }

        public PolicyListModel()
        {
            Roles = new List<RoleModel>();

            Groups = new List<GroupModel>();

            Policies = new List<PolicyModel>();
        }
    }

    public class PolicyModel
    {
        public string Role { get; set; }

        public string Group { get; set; }

        public string Effect { get; set; }
    }

    public class CreatePolicyModel
    {
        public string Role { get; set; }

        public string Group { get; set; }

        public bool Effect { get; set; }
    }

    public class DeletePolicyModel
    {
        public string Role { get; set; }

        public string Group { get; set; }
    }
}