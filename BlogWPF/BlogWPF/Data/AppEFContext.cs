using BlogWPF.Data.Entities.Identity;
using System.Data.Entity;

namespace BlogWPF.Data
{
    public class AppEFContext : DbContext
    {
        public DbSet<TokenEntity> Users { get; set; }

        public AppEFContext() : base("DefaultConnection")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}