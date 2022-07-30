using System.Collections.ObjectModel;
using Gitbang.Models;

namespace Gitbang.Managers.Interfaces;

public interface IReposIndexManager
{
    public ObservableCollection<TreeViewNodeModelBase> Repositories { get; }
    public ObservableCollection<TreeViewNodeModelBase> LoadReposIndex();
    public void SaveReposIndex();
}