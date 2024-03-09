using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using RoaylLibrary.Domain;

namespace RoaylLibrary.Data.DbContext.Client.EntityConfiguration
{
    public class BookEntityConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> entity)
        {
            entity.ToTable("Book");
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Title);
            entity.Property(e => e.Author);
            entity.Property(e => e.TotalCopies);
            entity.Property(e => e.CopiesInUse);
            entity.Property(e => e.BookType);
            entity.Property(e => e.Isbn);
            entity.Property(e => e.Category);
            entity.Property(e => e.Publisher);
        }
    }
}
