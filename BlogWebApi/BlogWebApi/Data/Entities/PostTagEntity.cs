using System.ComponentModel.DataAnnotations.Schema;

namespace BlogWebApi.Data.Entities
{
    [Table("tblPostTag")]
    public class PostTagEntity
    {
        [ForeignKey("Post")]
        public int PostId { get; set; }
        [ForeignKey("Tag")]
        public int TagId { get; set; }

        public virtual PostEntity Post { get; set; }
        public virtual TagEntity Tag { get; set; }
    }
}