using MDW.Entity;
using MDW.Repository.Interfaces;
using MDW.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using MDW.Tools;

namespace MDW.Services
{
    public class UserService : ServiceBase, IUserService
    {
        private IUserRepository Repository { get; set; }

        public UserService(IUserRepository repository)
        {
            Repository = repository;
        }

        public async Task<List<User>> GetUsers()
        {
            var result = new List<User>();

            result.AddRange(await Repository.GetUsers());

            result.ForEach(i => i.Password = null);

            return result;
        }

        public async Task<User> GetUser(string username)
        {
            User result = null;

            result = await Repository.GetUser(username);

            if (result != null)
                result.Password = null;

            return result;
        }

        public async Task<string> GetUserPassword(string username)
        {
            var result = string.Empty;

            result = await Repository.GetUserPassword(username);

            return result;
        }

        public async Task<bool> UserExists(string username)
        {
            var result = false;

            result = await Repository.UserExists(username);

            return result;
        }

        public async Task<bool> Authenticate(string username, string password)
        {
            var result = false;

            var user = await Repository.GetUser(username);

            if (user != null)
                result = user.Password == Hash.SHA256(password);

            return result;
        }

        public async Task<bool> ChangePassword(string username, string oldPassword, string newPassword)
        {
            var result = false;

            var user = await Repository.GetUser(username);

            if (user != null && user.Password == Hash.SHA256(oldPassword))
                result = await Repository.SetUserPassword(username, Hash.SHA256(newPassword));

            return result;
        }

        public async Task<bool> CreateUser(string firstname, string lastname, string username, string email, string role)
        {
            var result = false;

            var user = new User()
            {
                Enabled = true,
                FirstName = firstname,
                LastName = lastname,
                Username = username,
                Password = Hash.SHA256("password"),
                Email = email,
                Role = role,
                Gravatar = string.Format("https://www.gravatar.com/avatar/{0}", Hash.MD5(email.ToLower().Trim()).ToLower()),
                Theme = "default"
            };

            result = await Repository.CreateUser(user);

            return result;
        }

        public async Task<bool> UpdateUser(User user)
        {
            var result = false;

            var o = await Repository.GetUser(user.Username);

            user.Role = o.Role != "administrator" ? o.Role : user.Role;

            user.Password = await Repository.GetUserPassword(user.Username);

            user.Gravatar = string.Format("https://www.gravatar.com/avatar/{0}", Hash.MD5(user.Email.ToLower().Trim()).ToLower());

            result = await Repository.UpdateUser(user);

            return result;
        }

        public async Task<bool> DeleteUser(string username)
        {
            var result = false;

            result = await Repository.DeleteUser(username);

            return result;
        }
    }
}