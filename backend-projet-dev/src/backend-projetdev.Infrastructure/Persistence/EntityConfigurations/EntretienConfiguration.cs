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
    public class EntretienConfiguration : IEntityTypeConfiguration<Entretien>
    {
        public void Configure(EntityTypeBuilder<Entretien> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.DateEntretien)
                .IsRequired();

            builder.Property(e => e.Commentaire)
                .HasMaxLength(10000);

            builder.HasOne(e => e.Candidature)
                .WithMany(c => c.Entretiens)
                .HasForeignKey(e => e.CandidatureId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}