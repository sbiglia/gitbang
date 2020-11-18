using Gitbang.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gitbang.Services
{
    class DataRepository : IDataRepository
    {


        public void AddGroup(Group group)
        {
            throw new NotImplementedException();
        }

        public List<Group> GetAllGroups()
        {
            if(_groups == null)
            {
                _groups = GetAllGroupsFromLocalDb();
            }

            return _groups;
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

        private List<Group> GetAllGroupsFromLocalDb()
        {

        }
    }
}
