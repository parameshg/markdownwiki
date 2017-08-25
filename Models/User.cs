using System.Collections.Generic;

namespace MDW.Models
{
    public class UserListModel
    {
        public List<UserModel> Users { get; set; }

        public List<RoleModel> Roles { get; set; }

        public UserListModel()
        {
            Users = new List<UserModel>();
            Roles = new List<RoleModel>();
        }
    }

    public class UserModel
    {
        public bool Enabled { get; set; }

        public string Name { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string Role { get; set; }

        public string Theme { get; set; }

        public string Gravatar { get; set; }
    }

    public class CreateUserModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string Role { get; set; }        
    }

    public class UpdateUserModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string Role { get; set; }

        public string Theme { get; set; }
    }

    public class DeleteUserModel
    {
        public string Username { get; set; }
    }

    public class ChangePasswordModel
    {
        public string Username { get; set; }

        public string OldPassword { get; set; }

        public string NewPassword { get; set; }

        public string ConfirmPassword { get; set; }
    }
}