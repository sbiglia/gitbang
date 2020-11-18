using System.Collections.Generic;
using System.Collections.ObjectModel;
using Gitbang.Models;

namespace Gitbang.ViewModels
{
    public class ReposTreeViewModel : ViewModelBase
    {

        public ObservableCollection<Repository> Repositories;
        public ObservableCollection<Group> Groups;
        
        public ReposTreeViewModel(IEnumerable<Repository> repositories, IEnumerable<Group> groups)
        {
            Repositories = new ObservableCollection<Repository>(repositories);
            Groups = new ObservableCollection<Group>(groups);
        }
        
    }
}