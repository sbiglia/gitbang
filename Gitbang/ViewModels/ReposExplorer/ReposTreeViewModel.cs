using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Windows.Input;
using DynamicData;
using Gitbang.Models;
using Gitbang.Util;
using ReactiveUI;

namespace Gitbang.ViewModels.ReposExplorer
{
    public class ReposTreeViewModel : ViewModelBase
    {

        public ObservableCollection<TreeNodeModel> Nodes { get; }
        
        public ReposTreeViewModel(IEnumerable<TreeNodeModel> treeNodes)
        {
            Nodes =  new ObservableCollection<TreeNodeModel>(treeNodes);
            RenameCommand = ReactiveCommand.Create<TreeNodeModel>(RenameAction);
        }

        public ReactiveCommand<TreeNodeModel, Unit> RenameCommand { get; }

        void RenameAction(TreeNodeModel node)
        {
            if (node == null)
                throw new ArgumentNullException(nameof(node));
            
            
        }
        
    }
}