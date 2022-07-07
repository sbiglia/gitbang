using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gitbang.Core;
using Gitbang.Core.Base;
using Newtonsoft.Json;

namespace Gitbang.Models
{
    public class TreeViewNodeModel : ModelBase
    {

        public delegate void TreeNodeExpanded(TreeViewNodeModel node, bool isExpaded);
        public event TreeNodeExpanded? NodeExpanded;

        public TreeViewNodeModel()
        {
            Children = new ObservableCollection<TreeViewNodeModel>();
        }

        ~TreeViewNodeModel()
        {

        }
        
        public TreeNodeType? NodeType { get; init; }
        
        [JsonIgnore]
        public bool IsBranch => NodeType == TreeNodeType.Branch;
        
        [JsonIgnore]
        public bool IsLeaf => NodeType == TreeNodeType.Leaf;

        public string Name
        {
            get => Get<string>();
            set => Set(value);
        }

        public bool IsEditing
        {
            get => Get<bool>();
            set => Set(value);
        }
        
        public bool IsExpanded
        {
            get => Get<bool>();
            set
            {
                Set(value);
                var handler = NodeExpanded;
                handler?.Invoke(this,value);
            }
        }

        public ObservableCollection<TreeViewNodeModel> Children { get; init; }

    }
}
