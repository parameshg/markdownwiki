using MDW.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MDW.Services.Interfaces
{
    public interface IUserService : IService
    {
        Task<List<User>> GetUsers();

        Task<User> GetUser(string username);

        Task<string> GetUserPassword(string username);

        Task<bool> UserExists(string username);

        Task<bool> Authenticate(string username, string password);

        Task<bool> ChangePassword(string username, string oldPassword, string newPassword);

        Task<bool> CreateUser(string firstname, string lastname, string username, string email, string role);

        Task<bool> UpdateUser(User user);

        Task<bool> DeleteUser(string username);
    }
}