using Gitbang.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper;
using Gitbang.Services.ExecuteCommands;
using Gitbang.Services.Queries;
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
            var query = _executer.ExecuteCommand(_connStr, conn => conn.Query<Repository>(_commandText.GetGroups))
                .ToList();
            return query;
        }
    }
}
