using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gitbang.Core.Helpers;
using Gitbang.Core.Services;
using Gitbang.Core.Settings.Interfaces;
using Gitbang.Models;
using Newtonsoft.Json;
using Serilog;
using Splat;

namespace Gitbang.Managers
{
    internal class ReposIndexManager
    {
        private const string ReposIndexFilename = "ReposIndex.json";
        private const string ReposMutex = "366BE18F-FE69-437F-B8D2-7F085D957457";

        private ObservableCollection<TreeViewNodeModel>? _reposIndex;

        public ReposIndexManager(ISettingsManager settingsManager)
        {
            
        }


        public ObservableCollection<TreeViewNodeModel> Repositories
        {
            get
            {
                if(_reposIndex == null )
                    _reposIndex = new ObservableCollection<TreeViewNodeModel>();
                return _reposIndex;
            }
        }

        public  ObservableCollection<TreeViewNodeModel> LoadReposIndex()
        {
            //var repoTree = LoadRepoTree()  
        }

        public List<TreeViewNodeModel> LoadReposIndex(string folder)
        {
            var path = Path.Combine(Locator.Current.GetService<IEnvironmentService>().GetApplicationDataDirectory(),
                ReposIndexFilename);

            var retList = new List<TreeViewNodeModel>();

            var fileInfo = new FileInfo(path);
            
            if (!fileInfo.Exists)
            {
                Log.Logger.Debug($"ReposIndex.json file doesn't exists.");
                return retList;
            }

            using var mutex = new MutexProtector(ReposMutex);
            var deserialize = JsonConvert.DeserializeObject<List<TreeViewNodeModel>>(File.ReadAllText(fileInfo.FullName));
            
            return deserialize ?? retList;
        }

        public void SaveReposIndex()
        {
            if(_reposIndex == null)
                return;


        }

    }
}
