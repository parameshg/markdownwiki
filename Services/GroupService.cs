using MDW.Entity;
using MDW.Repository.Interfaces;
using MDW.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MDW.Services
{
    public class GroupService : ServiceBase, IGroupService
    {
        private IGroupRepository Repository { get; set; }

        public GroupService(IGroupRepository repository)
        {
            Repository = repository;
        }

        public async Task<List<Group>> GetGroups()
        {
            var result = new List<Group>();

            result.AddRange(await Repository.GetGroups());

            return result;
        }

        public async Task<Group> GetGroup(string name)
        {
            Group result = null;

            result = await Repository.GetGroup(name);

            return result;
        }

        public async Task<bool> CreateGroup(string name, bool builtin = false)
        {
            var result = false;

            result = await Repository.CreateGroup(new Group() { Name = name, Builtin = builtin });

            return result;
        }

        public async Task<bool> DeleteGroup(string name)
        {
            var result = false;

            var role = await Repository.GetGroup(name);

            if (role.Builtin)
                throw new Exception("Cannot delete builtin groups");

            result = await Repository.DeleteGroup(name);

            return result;
        }
    }
}