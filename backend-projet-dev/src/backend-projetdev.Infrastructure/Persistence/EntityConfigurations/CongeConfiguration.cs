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
    public class CongeConfiguration : IEntityTypeConfiguration<Conge>
    {
        public void Configure(EntityTypeBuilder<Conge> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.DateDebut)
                .IsRequired();

            builder.Property(c => c.DateFin)
                .IsRequired();

            builder.Property(c => c.Type)
                .HasMaxLength(100)
                .IsRequired();

            builder.HasOne(c => c.Employe)
                .WithMany(e => e.Conges)
                .HasForeignKey(c => c.EmployeId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}