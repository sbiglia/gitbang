using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Gitbang.Core.Helpers;
using Gitbang.Core.Services;
using Gitbang.Core.Settings.Interfaces;
using Gitbang.Managers.Interfaces;
using Gitbang.Models;
using Newtonsoft.Json;
using Serilog;
using Splat;

namespace Gitbang.Managers
{
    public class ReposIndexManager : IReposIndexManager
    {
        private const string ReposIndexFilename = "ReposIndex.json";
        private const string ReposMutex = "366BE18F-FE69-437F-B8D2-7F085D957457";

        private ObservableCollection<TreeViewNodeModel>? _reposIndex;

        public ReposIndexManager()
        {
            
        }


        public ObservableCollection<TreeViewNodeModel> Repositories
        {
            get
            {
                if (_reposIndex == null)
                    _reposIndex = LoadReposIndex();
                return _reposIndex;
            }
        }
        
        public ObservableCollection<TreeViewNodeModel> LoadReposIndex()
        {
            var path = Path.Combine(Locator.Current.GetService<IEnvironmentService>().GetApplicationDataDirectory(),
                ReposIndexFilename);

            var retList = new ObservableCollection<TreeViewNodeModel>();

            var fileInfo = new FileInfo(path);
            
            if (!fileInfo.Exists)
            {
                Log.Logger.Debug($"ReposIndex.json file doesn't exists.");
                return retList;
            }

            try
            {

                using var mutex = new MutexProtector(ReposMutex);
                var deserialize =
                    JsonConvert.DeserializeObject<ObservableCollection<TreeViewNodeModel>>(
                        File.ReadAllText(fileInfo.FullName));
                return deserialize ?? retList;

            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex, $"Failed to load ReposIndex from file: {path}");
            }
            
            return retList;
        }

        public void SaveReposIndex()
        {
            if(_reposIndex == null)
                return;

            var path = Path.Combine(Locator.Current.GetService<IEnvironmentService>().GetApplicationDataDirectory(), ReposIndexFilename);

            using var tempFile = new TempFile();
            var saved = false;
            
            try
            {
                var output = JsonConvert.SerializeObject(_reposIndex);
                File.WriteAllText(tempFile.FilePath, output);
                saved = true;

            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex, $"Failed to save RepoIndex to:{tempFile.FilePath}");
            }

            if(!saved)
                return;

            using var mutex = new MutexProtector(ReposMutex);

            try
            {
                File.Copy(tempFile.FilePath, path, true);
            }
            catch (Exception ex)
            {
                Log.Logger.Error(ex, $"Failed to copy temp RepoIndex file to:{path}");
            }
            
        }

    }
}
