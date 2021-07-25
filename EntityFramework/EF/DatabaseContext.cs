using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace EntityFramework
{
    public class DatabaseContext: DbContext
    {
        public DbSet<NewsEntity> News { get; set; }
        public DatabaseContext()
        {
            Database.EnsureCreated();
        }
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }
    }
}
