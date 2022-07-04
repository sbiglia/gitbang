using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gitbang.Models;

namespace Gitbang.Managers
{
    internal class RepoTreeManager
    {
        private const string RepositoriesFileName = "repositories.json";

        private ObservableCollection<TreeViewNodeModel> _repositories;

        public RepoTreeManager()
        {

        }


        public ObservableCollection<TreeViewNodeModel> Repositories
        {
            get
            {
                if( _repositories == null )
                    _repositories = new ObservableCollection<TreeViewNodeModel>();
                return _repositories;
            }
        }

        public  ObservableCollection<TreeViewNodeModel> LoadRepoTree()
        {
            //var repoTree = LoadRepoTree()  
        }

        public List<TreeViewNodeModel> LoadRepoTree(string folder)
        {
            //var path = 
        }

    }
}
