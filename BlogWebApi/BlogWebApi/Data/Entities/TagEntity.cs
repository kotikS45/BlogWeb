using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BlogWebApi.Data.Entities
{
    [Table("tblTags")]
    public class TagEntity : BaseEntity<int>
    {
        [Required, StringLength(255)]
        public string Name { get; set; }
        [StringLength(200)]
        public string UrlSlug { get; set; }
        [Required, StringLength(1000)]
        public string Description { get; set; }
        public virtual ICollection<PostEntity> Posts { get; set; }
    }
}
