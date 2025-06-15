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
    public class EmployeConfiguration : IEntityTypeConfiguration<Employe>
    {
        public void Configure(EntityTypeBuilder<Employe> builder)
        {
            // Hérite déjà de Personne

            builder.HasMany(e => e.Inscriptions)
                .WithOne(i => i.Employe)
                .HasForeignKey(i => i.EmployeId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(e => e.Evaluations)
                .WithOne(e => e.Employe)
                .HasForeignKey(e => e.EmployeId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(e => e.Conges)
                .WithOne(c => c.Employe)
                .HasForeignKey(c => c.EmployeId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(e => e.Equipe)
                .WithMany(eq => eq.Employes)
                .HasForeignKey(e => e.EquipeId);
        }
    }
}