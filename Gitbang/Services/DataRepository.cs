using Gitbang.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Dapper;
using Gitbang.Services.ExecuteCommands;
using Gitbang.Services.Queries;
using Gitbang.Util;
using Microsoft.Extensions.Configuration;

namespace Gitbang.Services
{
    class DataRepository : IDataRepository
    {
        private readonly IConfiguration _configuration;
        private readonly ICommandText _commandText;
        private readonly string _connStr;
        private readonly IExecuter _executer;

        public DataRepository(IConfiguration configuration, ICommandText commandText, IExecuter executer)
        {
            _configuration = configuration;
            _commandText = commandText;
            _executer = executer;
            _connStr = _configuration.GetConnectionString("Gitbang");
        }
        
        public void AddGroup(Group group)
        {
            throw new NotImplementedException();
        }

        public List<Group> GetAllGroups()
        {
            var query = _executer.ExecuteCommand(_connStr,
                conn => conn.Query<Group>(_commandText.GetGroups)).ToList();
            return query;
        }

        public List<TreeNodeModel> GetAllTreeNodeModels()
        {
            var groups = GetAllGroups();
            var repos = GetAllRepositories();

            var rootGroups = groups.Where(x => x.ParentGroupId == 0);
            var nodes = new List<TreeNodeModel>();
            
            foreach (var group in rootGroups)
            {
                nodes.Add(new TreeNodeModel()
                {
                    ItemType = TreeNodeItemType.Group,
                    Name = group.Name,
                    Children = new ObservableCollection<TreeNodeModel>(GetAllChildNodes(group.Id, groups, repos))
                });
            }

            return nodes;
        }

        private List<TreeNodeModel> GetAllChildNodes(int groupId, List<Group> groups, List<Repository> repos)
        {
            var childRepos = repos.Where(x => x.GroupId == groupId);
            var childGroups = groups.Where(x => x.ParentGroupId == groupId);

            var nodes = new List<TreeNodeModel>();

            foreach (var childGroup in childGroups)
            {
                nodes.Add(new TreeNodeModel()
                {
                    ItemType = TreeNodeItemType.Group,
                    Name = childGroup.Name,
                    Children = new ObservableCollection<TreeNodeModel>( GetAllChildNodes(childGroup.Id, groups, repos)),
                });
            }

            foreach (var childRepo in childRepos)
            {
                nodes.Add(new TreeNodeModel()
                {
                    ItemType = TreeNodeItemType.Repository,
                    Name = childRepo.Name,
                    Children = null
                });
            }

            return nodes;
        }

        public Group GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void RemoveGroup(Group group)
        {
            throw new NotImplementedException();
        }

        public void UpdateGroup(Group group)
        {
            throw new NotImplementedException();
        }

        public List<Repository> GetAllRepositories()
        {
            var query = _executer.ExecuteCommand(_connStr, conn => conn.Query<Repository>(_commandText.GetRepositories))
                .ToList();
            return query;
        }
    }
}
