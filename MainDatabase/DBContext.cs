using MainDatabase.dbo.Tables;
using Microsoft.EntityFrameworkCore;

namespace MainDatabase
{
    public class DBContext: DbContext
    {
        public DBContext(DbContextOptions<DBContext> options)
            : base(options) 
        {
         
        }

        public DbSet<Article> Articles { get; set; }
        public DbSet<Source> Sources { get; set; }
        public DbSet<Source> News { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Article>().ToTable("Article");
            modelBuilder.Entity<Source>().ToTable("Source");
            modelBuilder.Entity<Source>().ToTable("News");
        }

    }
}