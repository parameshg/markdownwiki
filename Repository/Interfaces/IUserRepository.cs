using MDW.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MDW.Repository.Interfaces
{
    public interface IUserRepository : IRepository
    {
        Task<List<User>> GetUsers();

        Task<User> GetUser(string username);

        Task<string> GetUserPassword(string username);

        Task<bool> SetUserPassword(string username, string password);

        Task<bool> UserExists(string username);

        Task<bool> CreateUser(User user);

        Task<bool> UpdateUser(User user);

        Task<bool> DeleteUser(string username);
    }
}