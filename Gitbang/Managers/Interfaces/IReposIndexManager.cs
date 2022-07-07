using System.Collections.ObjectModel;
using Gitbang.Models;

namespace Gitbang.Managers.Interfaces;

public interface IReposIndexManager
{
    public ObservableCollection<TreeViewNodeModel> Repositories { get; }
    public ObservableCollection<TreeViewNodeModel> LoadReposIndex();
    public void SaveReposIndex();
}