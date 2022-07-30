using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Gitbang.Core;
using Gitbang.Core.Base;
using Newtonsoft.Json;
using ReactiveUI;

namespace Gitbang.Models
{
    public class TreeViewNodeModelBase : ModelBase
    {

        public delegate void TreeNodeExpanded(TreeViewNodeModelBase node, bool isExpaded);
        public event TreeNodeExpanded? NodeExpanded;

        private string _nameBeforeEditing = "";

        public TreeViewNodeModelBase()
        {
            Children = new ObservableCollection<TreeViewNodeModelBase>();
            RenameCommand = ReactiveCommand.Create(Rename);
            CancelEditCommand = ReactiveCommand.Create(CancelEdit);
        }

        ~TreeViewNodeModelBase()
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
            set
            {
                if(!string.IsNullOrEmpty(value))
                    Set(value);
            }
        }

        public bool IsEditing
        {
            get => Get<bool>();
            set
            {
                if (value)
                    _nameBeforeEditing = Name;

                Set(value);

            }
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

        public ObservableCollection<TreeViewNodeModelBase> Children { get; init; }

        public ICommand RenameCommand { get; }
        public ICommand CancelEditCommand { get; }

        public void Rename()
        {
            IsEditing = false;

/*            if (string.IsNullOrEmpty(Name))
            {
                Name = _nameBeforeEditing;
                return;
            }*/

        }

        public void CancelEdit()
        {
            IsEditing = false;
            Name = _nameBeforeEditing;
        }

    }
}
