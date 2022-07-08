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
    public class TreeViewNodeModel : ModelBase
    {

        public delegate void TreeNodeExpanded(TreeViewNodeModel node, bool isExpaded);
        public event TreeNodeExpanded? NodeExpanded;

        private string _nameBeforeEditing = "";

        public TreeViewNodeModel()
        {
            Children = new ObservableCollection<TreeViewNodeModel>();
            RenameCommand = ReactiveCommand.Create<string>(Rename);
            CancelEditCommand = ReactiveCommand.Create(CancelEdit);
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

        public ObservableCollection<TreeViewNodeModel> Children { get; init; }

        public ICommand RenameCommand { get; }
        public ICommand CancelEditCommand { get; }

        public void Rename(string newName)
        {
            IsEditing = false;

            if (string.IsNullOrEmpty(newName))
            {
                Name = _nameBeforeEditing;
                return;
            }

            Name = newName;

        }

        public void CancelEdit()
        {
            IsEditing = false;
            Name = _nameBeforeEditing;
        }

    }
}
