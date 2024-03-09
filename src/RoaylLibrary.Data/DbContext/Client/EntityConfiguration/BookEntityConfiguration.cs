using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using RoaylLibrary.Domain;

namespace RoaylLibrary.Data.DbContext.Client.EntityConfiguration
{
    public class BookEntityConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> entity)
        {
            entity.HasKey(e => e.Id);
        }
    }
}
