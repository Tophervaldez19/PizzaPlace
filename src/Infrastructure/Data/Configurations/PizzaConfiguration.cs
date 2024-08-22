using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PizzaPlace.Domain.Entities;

namespace PizzaPlace.Infrastructure.Data.Configurations;
public class PizzaConfiguration : IEntityTypeConfiguration<Pizza>
{
    public void Configure(EntityTypeBuilder<Pizza> builder)
    {
        builder.Property(x => x.Id)
            .ValueGeneratedNever();

        builder.HasOne(x => x.PizzaType)
            .WithMany(x => x.Pizzas)
            .HasForeignKey(x => x.PizzaTypeId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
