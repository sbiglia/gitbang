using System;
using System.Collections.Generic;
using System.Text;
using Gitbang.Services;

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
