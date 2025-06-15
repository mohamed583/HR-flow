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
    public class FormationConfiguration : IEntityTypeConfiguration<Formation>
    {
        public void Configure(EntityTypeBuilder<Formation> builder)
        {
            builder.HasKey(f => f.Id);

            builder.Property(f => f.Titre)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(f => f.DateDebut)
                .IsRequired();

            builder.Property(f => f.DateFin)
                .IsRequired();

            builder.HasOne(f => f.Formateur)
                .WithMany(f => f.Formations)
                .HasForeignKey(f => f.FormateurId);

            builder.HasMany(f => f.Inscriptions)
                .WithOne(i => i.Formation)
                .HasForeignKey(i => i.FormationId);
        }
    }
}