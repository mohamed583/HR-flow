using backend_projetdev.Domain.Entities;
using Infrastructure.Authentication;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace backend_projetdev.Infrastructure.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Personne> Personnes { get; set; }
        public DbSet<Candidat> Candidats { get; set; }
        public DbSet<Employe> Employes { get; set; }
        public DbSet<Formateur> Formateurs { get; set; }
        public DbSet<Formation> Formations { get; set; }
        public DbSet<Inscription> Inscriptions { get; set; }
        public DbSet<Evaluation> Evaluations { get; set; }
        public DbSet<Paie> Paies { get; set; }
        public DbSet<Conge> Conges { get; set; }
        public DbSet<Departement> Departements { get; set; }
        public DbSet<Equipe> Equipes { get; set; }
        public DbSet<Poste> Postes { get; set; }
        public DbSet<Candidature> Candidatures { get; set; }
        public DbSet<Entretien> Entretiens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure l'héritage de Personne
            modelBuilder.Entity<Candidat>().HasBaseType<Personne>();
            modelBuilder.Entity<Employe>().HasBaseType<Personne>();
            modelBuilder.Entity<Formateur>().HasBaseType<Personne>();
            modelBuilder.Entity<Personne>().ToTable("Personnes");

            // Applique toutes les configurations à partir du dossier EntityConfigurations
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
