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
    public class PaieConfiguration : IEntityTypeConfiguration<Paie>
    {
        public void Configure(EntityTypeBuilder<Paie> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Montant)
                .IsRequired();

            builder.Property(p => p.DatePaie)
                .IsRequired();

            builder.HasOne(p => p.Personne)
                .WithMany(pers => pers.Paies)
                .HasForeignKey(p => p.PersonneId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}