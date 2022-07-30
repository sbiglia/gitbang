namespace Gitbang.Models;

public class RepositoryNodeModel : TreeViewNodeModelBase
{

    public string Path
    {
        get => Get<string>();
        set => Set(value);
    }
    
}