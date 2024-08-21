using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PizzaPlace.Domain.Entities;

namespace PizzaPlace.Infrastructure.Data.Configurations;
public class PizzaTypeConfiguration : IEntityTypeConfiguration<PizzaType>
{
    public void Configure(EntityTypeBuilder<PizzaType> builder)
    {
        builder.Property(x => x.Id)
            .ValueGeneratedNever();
    }
}
