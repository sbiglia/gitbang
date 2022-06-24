using System;
using System.Collections.Generic;
using System.Text;
using JetBrains.Annotations;

namespace Gitbang.Core
{
    /// <summary>
    /// A group of code repositories and others groups.
    /// </summary>
    internal class RepoGroup
    {
        /// <summary>
        /// used to identify de group when saving it to the database.
        /// </summary>
        private int _id;

        public List<RepoGroup> ChildGroups { get; } = new();
        public List<GitRepo> ChildRepos { get; } = new();


        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException(nameof(value));
                }

                _name = value;
            }
        }

        public RepoGroup(string name)
        {
            _id = -1;
            Name = name;
        }

        


    }
}
