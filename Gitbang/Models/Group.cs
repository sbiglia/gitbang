namespace Gitbang.Models
{
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ParentGroupId { get; set; }
    }
}