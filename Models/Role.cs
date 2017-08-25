using System.Collections.Generic;

namespace MDW.Models
{
    public class RoleListModel
    {
        public List<RoleModel> Roles { get; set; }

        public RoleListModel()
        {
            Roles = new List<RoleModel>();
        }
    }

    public class RoleModel
    {
        public bool Builtin { get; set; }

        public string Name { get; set; }
    }

    public class CreateRoleModel
    {
        public string Name { get; set; }
    }

    public class DeleteRoleModel
    {
        public string Name { get; set; }
    }
}