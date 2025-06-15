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
    public class CandidatConfiguration : IEntityTypeConfiguration<Candidat>
    {
        public void Configure(EntityTypeBuilder<Candidat> builder)
        {
            // Inheritance est déjà configurée via PersonneConfiguration

            builder.HasMany(c => c.Candidatures)
                .WithOne(cp => cp.Candidat)
                .HasForeignKey(cp => cp.CandidatId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }

}
