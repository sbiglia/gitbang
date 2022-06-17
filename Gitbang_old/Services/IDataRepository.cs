using System.Collections.Generic;
using Gitbang_old.Models;

namespace Gitbang_old.Services
{
    public interface IDataRepository
    {        
        List<Group> GetAllGroups();
        void AddGroup(Group group);
        void UpdateGroup(Group group);
        void RemoveGroup(Group group);
        Group GetById(int id);

        List<Repository> GetAllRepositories();
        List<TreeNodeModel> GetAllTreeNodeModels();

    }
}
