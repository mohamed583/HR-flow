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
    public class InscriptionConfiguration : IEntityTypeConfiguration<Inscription>
    {
        public void Configure(EntityTypeBuilder<Inscription> builder)
        {
            builder.HasKey(i => i.Id);

            builder.Property(i => i.DateInscription)
                .IsRequired();

            builder.HasOne(i => i.Employe)
                .WithMany(e => e.Inscriptions)
                .HasForeignKey(i => i.EmployeId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(i => i.Formation)
                .WithMany(f => f.Inscriptions)
                .HasForeignKey(i => i.FormationId);
        }
    }
}