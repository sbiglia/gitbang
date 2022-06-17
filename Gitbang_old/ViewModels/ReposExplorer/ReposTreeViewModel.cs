using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reactive;
using Gitbang_old.Models;
using ReactiveUI;

namespace Gitbang_old.ViewModels.ReposExplorer
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