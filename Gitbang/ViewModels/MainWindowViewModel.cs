using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Gitbang.Models;
using Gitbang.Services;
using Gitbang.ViewModels.ReposExplorer;

namespace Gitbang.ViewModels
{

    public class MainWindowViewModel : ViewModelBase
    {
        public ReposTreeViewModel Repos { get; }
        private IDataRepository _dataRepository;

        public MainWindowViewModel(IDataRepository dataRepository)
        {
            _dataRepository = dataRepository;
            Repos = new ReposTreeViewModel(dataRepository.GetAllTreeNodeModels());
        }
    }
}
