using Microsoft.AspNet.Identity;

namespace MDW.Security
{
    public class WikiUserManager : UserManager<WikiUser, string>
    {
        public WikiUserManager(IUserStore<WikiUser, string> store) 
            : base(store)
        {
            PasswordHasher = new WikiPasswordHasher();
        }
    }
}