using MDW.Entity;
using MDW.Repository.Interfaces;
using MDW.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MDW.Services
{
    public class RoleService : ServiceBase, IRoleService
    {
        private IRoleRepository Repository { get; set; }

        public RoleService(IRoleRepository repository)
        {
            Repository = repository;
        }

        public async Task<List<Role>> GetRoles()
        {
            var result = new List<Role>();

            result.AddRange(await Repository.GetRoles());

            return result;
        }

        public async Task<Role> GetRole(string name)
        {
            Role result = null;

            result = await Repository.GetRole(name);

            return result;
        }

        public async Task<bool> CreateRole(string name, bool builtin = false)
        {
            var result = false;

            result = await Repository.CreateRole(new Role() { Name = name, Builtin = builtin });

            return result;
        }

        public async Task<bool> DeleteRole(string name)
        {
            var result = false;

            var role = await Repository.GetRole(name);

            if (role.Builtin)
                throw new Exception("Cannot delete builtin roles");

            result = await Repository.DeleteRole(name);

            return result;
        }
    }
}