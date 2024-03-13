using BlogWebApi.Data.Entities.Identity;

namespace BlogWebApi.Models.Account
{
    public class AccountItemViewModel
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Image { get; set; }
        public ICollection<string> Roles { get; set; }
    }
}
