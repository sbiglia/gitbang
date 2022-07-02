using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gitbang.Models;

namespace Gitbang.Managers
{
    internal class RepositoriesManager
    {
        private const string RepositoriesFileName = "repositories.json";

        private ObservableCollection<TreeViewNodeModel> _repositories;

        public RepositoriesManager()
        {

        }

    }
}
