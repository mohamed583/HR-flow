using backend_projetdev.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace backend_projetdev.Infrastructure.Persistence
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            // Construction manuelle de la configuration pour accéder à appsettings.json
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory()) // important : exécution depuis dossier du projet
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            // Récupération de la chaîne de connexion
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            // Configuration des options du DbContext
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}
