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
    public class EvaluationConfiguration : IEntityTypeConfiguration<Evaluation>
    {
        public void Configure(EntityTypeBuilder<Evaluation> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Note)
                .IsRequired();

            builder.Property(e => e.CommentairesEmploye)
                .HasMaxLength(1000);

            builder.Property(e => e.CommentairesResponsable)
                .HasMaxLength(1000);

            builder.Property(e => e.DateEvaluation)
                .IsRequired();

            builder.HasOne(e => e.Employe)
                .WithMany(emp => emp.Evaluations)
                .HasForeignKey(e => e.EmployeId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}