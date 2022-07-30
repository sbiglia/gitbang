using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Gitbang.Core.Base;
using Gitbang.Managers.Interfaces;
using Gitbang.Models;
using ReactiveUI;

namespace Gitbang.ViewModels
{
    public class MainViewViewModel : ViewModelBase
    {
        private IReposIndexManager _reposIndexManager;

        public string WindowTitle
        {
            get => Get<string>();
            private set => Set(value);
        }

        public ICommand RenameCommand { get; }


        public MainViewViewModel()
        {

        }

        public MainViewViewModel(IReposIndexManager reposIndexManager)
        {

            _reposIndexManager = reposIndexManager;

            WindowTitle = Constants.AppName;
            
            Node = new FolderNodeModel()
            {
                Children = _reposIndexManager.Repositories
            };
            
            var node = new FolderNodeModel()
            {
                NodeType = TreeNodeType.Branch,
                Name = "Group1"
            };

            var node1 = new FolderNodeModel()
            {
                NodeType = TreeNodeType.Branch,
                Name = "Group2"
            };

            var node2 = new RepositoryNodeModel()
            {
                NodeType = TreeNodeType.Leaf,
                Name = "Repo1",
                Path = "c:\\sarasa\\Repo1"
            };

            var node3 = new RepositoryNodeModel()
            {
                NodeType = TreeNodeType.Leaf,
                Name = "Repo2",
                Path = "c:\\sareasaasa\\asdasdasdas\\asdasds\\"
            };
            
            var node4 = new RepositoryNodeModel()
            {
                NodeType = TreeNodeType.Leaf,
                Name = "Repo3",
                Path = "c:\\sdfsdfsd\\sdfsdfs\\dfsdfsdf"
            };
            
            node.Children.Add(node3);
            node.Children.Add(node4);
            node.Children.Add(node1);
            Node.Children.Add(node);
            Node.Children.Add(node2);

            RenameCommand = ReactiveCommand.Create<TreeViewNodeModelBase?>(Rename);

        }

        public TreeViewNodeModelBase Node { get; init; }


        public void Rename(TreeViewNodeModelBase? selectedItem)
        {

            //var item = selectedItem as TreeViewNodeModel;

            if(selectedItem == null)
                return;


            selectedItem.IsEditing = true;

        }

    }
}
