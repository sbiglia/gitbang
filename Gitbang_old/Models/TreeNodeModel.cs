using System.Collections.ObjectModel;
using Gitbang_old.Util;

namespace Gitbang_old.Models
{
    public class TreeNodeModel : ModelBase
    {
        public ObservableCollection<TreeNodeModel> Children { get; set; }

        public string Name
        {
            get => Get<string>();
            set => Set(value);
        }
        
        public TreeNodeItemType ItemType { get; set; }

        public bool IsGroup => ItemType == TreeNodeItemType.Group;
        public bool IsRepository => ItemType == TreeNodeItemType.Repository;
                

    }
}