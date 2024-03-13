using BlogWPF.Data.Entities.Identity;
using System.Data.Entity;

namespace BlogWPF.Data
{
    public class AppEFContext : DbContext
    {
        public DbSet<UserEntity> Users { get; set; }

        public AppEFContext() : base("DefaultConnection")
        {
        }
    }
}