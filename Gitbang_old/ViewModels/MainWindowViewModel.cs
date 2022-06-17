using Gitbang_old.Services;
using Gitbang_old.ViewModels.ReposExplorer;

namespace Gitbang_old.ViewModels
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
