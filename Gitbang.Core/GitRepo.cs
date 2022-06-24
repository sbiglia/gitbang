using System;
using System.Collections.Generic;
using System.Text;

namespace Gitbang.Core
{
    public class GitRepo
    {
        private int _id;

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

        public GitRepo(string name)
        {
            _id = -1;
        }
    }
}
