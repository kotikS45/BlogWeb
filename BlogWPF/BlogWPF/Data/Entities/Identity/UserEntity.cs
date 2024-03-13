using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogWPF.Data.Entities.Identity
{
    [Table("Users")]
    public class UserEntity
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Image { get; set; }
        public string Token { get; set; }
        public string Roles { get; set; }
    }
}
