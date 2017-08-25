using System.Collections.Generic;

namespace MDW.Models
{
    public class GroupListModel
    {
        public List<GroupModel> Groups { get; set; }

        public GroupListModel()
        {
            Groups = new List<GroupModel>();
        }
    }

    public class GroupModel
    {
        public bool Builtin { get; set; }

        public string Name { get; set; }
    }

    public class CreateGroupModel
    {
        public string Name { get; set; }
    }

    public class DeleteGroupModel
    {
        public string Name { get; set; }
    }
}