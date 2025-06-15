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
    public class PersonneConfiguration : IEntityTypeConfiguration<Personne>
    {
        public void Configure(EntityTypeBuilder<Personne> builder)
        {
            builder.ToTable("Personnes");

            // Table per hierarchy (TPH) mapping
            builder.HasDiscriminator<string>("Discriminator")
                .HasValue<Personne>("Personne")
                .HasValue<Candidat>("Candidat")
                .HasValue<Employe>("Employe")
                .HasValue<Formateur>("Formateur");

            // Relations communes définies dans les entités enfants
        }
    }
}