using Microsoft.EntityFrameworkCore;

using RoaylLibrary.Domain;
using RoaylLibrary.Data.DbContext.Client.EntityConfiguration;

namespace RoaylLibrary.Data.DbContext.Client
{
    public class RoyalLibraryDbContext(DbContextOptions options) : Microsoft.EntityFrameworkCore.DbContext(options)
    {
        public virtual DbSet<Book> Books { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new BookEntityConfiguration());
        }
    }
}
