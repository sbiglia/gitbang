using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gitbang.Core;
using Gitbang.Core.Base;

namespace Gitbang.Models
{
    public class TreeViewNodeModel : ModelBase
    {

        public delegate void TreeNodeExpanded(TreeViewNodeModel node, bool isExpaded);
        public event TreeNodeExpanded NodeExpanded;

        public TreeViewNodeModel(TreeNodeType? nodeType)
        {
            Children = new ObservableCollection<TreeViewNodeModel>();
            NodeType = nodeType;
        }

        ~TreeViewNodeModel()
        {

        }

        internal TreeNodeType? NodeType { get; }

        public bool IsBranch => NodeType == TreeNodeType.Branch;

        public bool IsLeaf => NodeType == TreeNodeType.Leaf;

        public string Name
        {
            get => Get<string>();
            set => Set(value);
        }

        public bool IsExpanded
        {
            get => Get<bool>();
            set
            {
                Set(value);
                var handler = NodeExpanded;
                if(handler == null)
                    return;
                handler.Invoke(this,value);
            }
        }

        public ObservableCollection<TreeViewNodeModel> Children { get; }

    }
}
