using MDW.Repository;
using MDW.Services;
using MDW.Services.Interfaces;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;

namespace MDW.Security
{
    public class WikiUserStore : IUserStore<WikiUser, string>, IUserPasswordStore<WikiUser>
    {
        private IUserService Users { get; set; }

        public WikiUserStore()
        {
            Users = new UserService(new UserRepository());
        }

        public async Task<WikiUser> FindByIdAsync(string userId)
        {
            var result = new WikiUser();

            var user = await Users.GetUser(userId);

            if (user != null)
                result.UserName = user.Username;

            return result;
        }

        public async Task<WikiUser> FindByNameAsync(string userName)
        {
            var result = new WikiUser();

            var user = await Users.GetUser(userName);

            if (user != null)
                result.UserName = user.Username;

            return result;
        }

        public async Task<bool> HasPasswordAsync(WikiUser user)
        {
            var result = false;

            var o = await Users.GetUser(user.UserName);

            result = o != null && !string.IsNullOrEmpty(o.Password);

            return result;
        }

        public async Task<string> GetPasswordHashAsync(WikiUser user)
        {
            var result = string.Empty;

            result = await Users.GetUserPassword(user.UserName);

            return result;
        }

        public async Task SetPasswordHashAsync(WikiUser user, string passwordHash)
        {
            var o = await Users.GetUser(user.UserName);

            if (o != null)
            {
                await Users.UpdateUser(new Entity.User()
                {
                    Enabled = o.Enabled,
                    Username = o.Username,
                    FirstName = o.FirstName,
                    LastName = o.LastName,
                    Email = o.Email,
                    Password = passwordHash,
                    Theme = o.Theme
                });
            }
        }

        public async Task CreateAsync(WikiUser user)
        {
            await Users.CreateUser(user.UserName, string.Empty, user.UserName, string.Empty, "user");
        }

        public async Task UpdateAsync(WikiUser user)
        {
            await Users.UpdateUser(new Entity.User()
            {
                Enabled = true,
                Username = user.UserName,
                FirstName = user.UserName,
                LastName = string.Empty,
                Email = string.Empty,
                Password = "password",
                Theme = "default"
            });
        }

        public async Task DeleteAsync(WikiUser user)
        {
            await Users.DeleteUser(user.UserName);
        }

        public void Dispose()
        {
        }
    }
}