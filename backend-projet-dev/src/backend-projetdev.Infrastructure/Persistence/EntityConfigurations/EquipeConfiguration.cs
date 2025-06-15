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
    public class EquipeConfiguration : IEntityTypeConfiguration<Equipe>
    {
        public void Configure(EntityTypeBuilder<Equipe> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Nom)
                .HasMaxLength(100)
                .IsRequired();

            builder.HasOne(e => e.Departement)
                .WithMany(d => d.Equipes)
                .HasForeignKey(e => e.DepartementId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(e => e.Employes)
                .WithOne(emp => emp.Equipe)
                .HasForeignKey(emp => emp.EquipeId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}