namespace Gitbang.Models
{
    public class Repository
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Group Group { get; set; }
    }
}