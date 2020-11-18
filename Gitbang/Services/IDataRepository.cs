using Gitbang.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gitbang.Services
{
    public interface IDataRepository
    {        
        List<Group> GetAllGroups();
        void AddGroup(Group group);
        void UpdateGroup(Group group);
        void RemoveGroup(Group group);
        Group GetById(int id);

        List<Repository> GetAllRepositories();

    }
}
