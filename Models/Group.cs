namespace Gitbang.Models
{
    public class Group
    {
        public string Name { get; set; }
        public Group Parent { get; set; }
    }
}