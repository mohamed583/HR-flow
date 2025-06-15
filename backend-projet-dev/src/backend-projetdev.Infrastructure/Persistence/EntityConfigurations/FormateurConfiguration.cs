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
    public class FormateurConfiguration : IEntityTypeConfiguration<Formateur>
    {
        public void Configure(EntityTypeBuilder<Formateur> builder)
        {
            // Hérite déjà de Personne

            builder.HasMany(f => f.Formations)
                .WithOne(f => f.Formateur)
                .HasForeignKey(f => f.FormateurId);
        }
    }
}