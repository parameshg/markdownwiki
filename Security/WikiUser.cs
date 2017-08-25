using Microsoft.AspNet.Identity;

namespace MDW.Security
{
    public class WikiUser : IUser<string>
    {
        public string Id { get { return UserName; } }

        public string UserName { get; set; }
    }
}