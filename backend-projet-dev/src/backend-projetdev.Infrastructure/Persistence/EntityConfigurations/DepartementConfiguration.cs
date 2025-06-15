using backend_projetdev.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backend_projetdev.Infrastructure.Persistence.EntityConfigurations
{
    public class DepartementConfiguration : IEntityTypeConfiguration<Departement>
    {
        public void Configure(EntityTypeBuilder<Departement> builder)
        {
            builder.HasKey(d => d.Id);

            builder.Property(d => d.Nom)
                .HasMaxLength(100)
                .IsRequired();

            builder.HasMany(d => d.Equipes)
                .WithOne(e => e.Departement)
                .HasForeignKey(e => e.DepartementId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
