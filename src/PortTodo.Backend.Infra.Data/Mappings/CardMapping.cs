using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PortTodo.Backend.Domain.Models;

namespace PortTodo.Backend.Infra.Data.Mappings
{
    public class CardMapping : IEntityTypeConfiguration<Card>
    {
        public void Configure(EntityTypeBuilder<Card> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(c => c.Name)
                .IsRequired();
        }
    }
}