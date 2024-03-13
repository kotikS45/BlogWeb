using System.Collections.Generic;

namespace BlogWPF.Data.Entities.Identity
{
    public class UserEntity : BaseEntity<int>
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Image { get; set; }
        public string Token { get; set; }
        public string Roles { get; set; }
    }
}
