using System.Collections.Generic;
using Gitbang.Models;

namespace Gitbang.Services
{
    public class Database
    {
        //public IEnumerable<Repository> GetRepositories() => new[] {new Repository() {Group = }};

        public IEnumerable<Group> GetGroups()
        {
            var group1 = new Group() {Name = "RootGroup1", Parent = null};
            var group2 = new Group() {Name = "RootGroup1", Parent = null};
            var group3 = new Group() {Name = "RootGroup1", Parent = null};
            var group4 = new Group() {Name = "RootGroup1", Parent = null};
            var group5 = new Group() {Name = "RootGroup1", Parent = null};
            var group6 = new Group() {Name = "RootGroup1", Parent = null};
            
            return new[] {group1, group2, group3, group4, group5, group6};
        }
    }
}