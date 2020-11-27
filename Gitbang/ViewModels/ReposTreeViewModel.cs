using System.Collections.Generic;
using System.Collections.ObjectModel;
using DynamicData;
using Gitbang.Models;
using Gitbang.Util;

namespace Gitbang.ViewModels
{
    public class ReposTreeViewModel : ViewModelBase
    {

        public ObservableCollection<TreeNodeModel> Nodes { get; }
        
        public ReposTreeViewModel(IEnumerable<TreeNodeModel> treeNodes)
        {
            Nodes =  new ObservableCollection<TreeNodeModel>(treeNodes);
        }       
        
    }
}