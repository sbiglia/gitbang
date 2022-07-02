using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gitbang.Core.Base;
using Gitbang.Models;

namespace Gitbang.ViewModels
{
    public class MainViewModel : ViewModelBase
    {

        public string WindowTitle
        {
            get => Get<string>();
            private set => Set(value);
        }

        public MainViewModel()
        {
            WindowTitle = Constants.AppName;
            Node = new TreeViewNodeModel(null);

            var node = new TreeViewNodeModel(TreeNodeType.Branch);
            node.Name = "Group1";
            var node1 = new TreeViewNodeModel(TreeNodeType.Branch);
            node1.Name = "Group2";

            var node2 = new TreeViewNodeModel(TreeNodeType.Leaf);
            var node3 = new TreeViewNodeModel(TreeNodeType.Leaf);
            var node4 = new TreeViewNodeModel(TreeNodeType.Leaf);

            node2.Name = "Repo1";
            node3.Name = "Repo2";
            node4.Name = "Repo3";

            node.Children.Add(node3);
            node.Children.Add(node4);
            node.Children.Add(node1);
            Node.Children.Add(node);
            Node.Children.Add(node2);

        }

        public TreeViewNodeModel Node { get; }

    }
}
