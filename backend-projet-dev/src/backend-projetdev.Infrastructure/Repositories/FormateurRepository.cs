using backend_projetdev.Application.Interfaces;
using backend_projetdev.Domain.Entities;
using backend_projetdev.Infrastructure.Persistence;
using Infrastructure.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace backend_projetdev.Infrastructure.Repositories
{
    public class FormateurRepository : IFormateurRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public FormateurRepository(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<List<Formateur>> GetAllAsync()
        {
            return await _context.Formateurs.ToListAsync();
        }

        public async Task<Formateur?> GetByIdAsync(string id)
        {
            return await _context.Formateurs.FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task<bool> CreateAsync(Formateur formateur, string password)
        {
            var applicationUser = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                UserName = formateur.Email,
                Email = formateur.Email,
                Nom = formateur.Nom,
                Prenom = formateur.Prenom,
                // autres propriétés si besoin
            };

            var result = await _userManager.CreateAsync(applicationUser, password);
            if (!result.Succeeded)
                return false;
            formateur.Id = applicationUser.Id;
            formateur.UserName = formateur.Email;
            await _context.Formateurs.AddAsync(formateur);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task AddAsync(Formateur formateur)
        {
            // Mettre à jour l'utilisateur ASP.NET
            var appUser = await _userManager.FindByIdAsync(formateur.Id);
            if (appUser != null)
            {
                appUser.Nom = formateur.Nom;
                appUser.Prenom = formateur.Prenom;

                var identityResult = await _userManager.UpdateAsync(appUser);
                if (!identityResult.Succeeded)
                {
                    // Tu peux logguer ou gérer l’erreur ici
                    throw new Exception("Échec de la mise à jour de l'utilisateur ASP.NET");
                }
            }
            await _context.Formateurs.AddAsync(formateur);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateAsync(Formateur formateur)
        {
            var appUser = await _userManager.FindByIdAsync(formateur.Id);
            if (appUser != null)
            {
                appUser.Nom = formateur.Nom;
                appUser.Prenom = formateur.Prenom;

                var identityResult = await _userManager.UpdateAsync(appUser);
                if (!identityResult.Succeeded)
                {
                    // Tu peux logguer ou gérer l’erreur ici
                    throw new Exception("Échec de la mise à jour de l'utilisateur ASP.NET");
                }
            }
            _context.Formateurs.Update(formateur);
            await Task.CompletedTask;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(Formateur formateur)
        {
            var user = await _userManager.FindByIdAsync(formateur.Id);
            if (user != null)
            {
                var result = await _userManager.DeleteAsync(user);
                if (!result.Succeeded)
                    return false;
            }

            _context.Formateurs.Remove(formateur);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
