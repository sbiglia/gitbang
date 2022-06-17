using System.ComponentModel.DataAnnotations.Schema;

namespace Gitbang_old.Models
{
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [Column("parent_group_id")]
        public int ParentGroupId { get; set; }
    }
}