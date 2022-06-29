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
    public class TreeNodeModel : ModelBase
    {

        public delegate void TreeNodeExpanded(TreeNodeModel node, bool isExpaded);
        public event TreeNodeExpanded NodeExpanded;

        public TreeNodeModel(TreeNodeType? nodeType)
        {
            Children = new ObservableCollection<TreeNodeModel>();
            NodeType = nodeType;
        }

        ~TreeNodeModel()
        {

        }

        internal TreeNodeType? NodeType { get; }

        public bool IsGroup => NodeType == TreeNodeType.Group;

        public bool IsRepository => NodeType == TreeNodeType.Repository;

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

        public ObservableCollection<TreeNodeModel> Children { get; }

    }
}
