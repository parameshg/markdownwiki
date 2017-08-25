using MDW.Entity;
using MDW.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MDW.Repository
{
    public class UserRepository : RepositoryBase, IUserRepository
    {
        public UserRepository()
        {

        }

        public async Task<List<User>> GetUsers()
        {
            var result = new List<User>();

            await Task.Run(() =>
            {
                result.AddRange(Database.GetCollection<User>().FindAll());
            });

            return result;
        }

        public async Task<User> GetUser(string username)
        {
            User result = null;

            await Task.Run(() =>
            {
                result = Database.GetCollection<User>().FindOne(i => i.Username.Equals(username));
            });

            return result;
        }

        public async Task<string> GetUserPassword(string username)
        {
            var result = string.Empty;

            await Task.Run(() =>
            {
                var user = Database.GetCollection<User>().FindOne(i => i.Username.Equals(username));

                result = user != null ? user.Password : string.Empty;
            });

            return result;
        }

        public async Task<bool> SetUserPassword(string username, string password)
        {
            var result = false;

            await Task.Run(() =>
            {
                var user = Database.GetCollection<User>().FindOne(i => i.Username.Equals(username));

                if (user != null)
                {
                    user.Password = password;
                    result = Database.GetCollection<User>().Update(user.Username, user);
                }
            });

            return result;
        }

        public async Task<bool> UserExists(string username)
        {
            var result = false;

            await Task.Run(() =>
            {
                result = Database.GetCollection<User>().Exists(i => i.Username.Equals(username));
            });

            return result;
        }

        public async Task<bool> CreateUser(User user)
        {
            var result = false;

            await Task.Run(() =>
            {
                result = Database.GetCollection<User>().Upsert(user.Username, user);
            });

            return result;
        }

        public async Task<bool> UpdateUser(User user)
        {
            var result = false;

            await Task.Run(() =>
            {
                result = Database.GetCollection<User>().Update(user.Username, user);
            });

            return result;
        }

        public async Task<bool> DeleteUser(string username)
        {
            var result = false;

            await Task.Run(() =>
            {
                result = Database.GetCollection<User>().Delete(i => i.Username.Equals(username)) > 0;
            });

            return result;
        }
    }
}