using Microsoft.EntityFrameworkCore;

using RoaylLibrary.Domain;
using RoaylLibrary.Data.DbContext.Client.EntityConfiguration;

namespace RoaylLibrary.Data.DbContext.Client
{
    public class RoyalLibraryDbContext(DbContextOptions<RoyalLibraryDbContext> options) : Microsoft.EntityFrameworkCore.DbContext(options)
    {
        public  DbSet<Book> Books { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BookEntityConfiguration());
        }
    }
}
