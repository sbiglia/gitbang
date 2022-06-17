using System.ComponentModel.DataAnnotations.Schema;

namespace Gitbang_old.Models
{
    public class Repository
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [Column("group_id")]
        public int GroupId { get; set; }
    }
}