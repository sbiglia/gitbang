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
            Node = new TreeNodeModel(null);

            var node = new TreeNodeModel(TreeNodeType.Group);
            node.Name = "Group1";
            var node1 = new TreeNodeModel(TreeNodeType.Group);
            node1.Name = "Group2";

            var node2 = new TreeNodeModel(TreeNodeType.Repository);
            var node3 = new TreeNodeModel(TreeNodeType.Repository);
            var node4 = new TreeNodeModel(TreeNodeType.Repository);

            node2.Name = "Repo1";
            node3.Name = "Repo2";
            node4.Name = "Repo3";

            node.Children.Add(node3);
            node.Children.Add(node4);
            node.Children.Add(node1);
            Node.Children.Add(node);
            Node.Children.Add(node2);

        }

        public TreeNodeModel Node { get; }

    }
}
