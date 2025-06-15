using backend_projetdev.Application.Interfaces;
using backend_projetdev.Domain.Entities;
using backend_projetdev.Infrastructure.Persistence;
using Infrastructure.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace backend_projetdev.Infrastructure.Repositories
{
    public class EmployeRepository : IEmployeRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public EmployeRepository(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<Employe?> GetByIdAsync(string id)
        {
            return await _context.Employes
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<Employe?> GetByEmailAsync(string email)
        {
            return await _context.Employes
                .FirstOrDefaultAsync(e => e.Email == email);
        }

        public async Task<List<Employe>> GetAllAsync()
        {
            return await _context.Employes
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<List<Employe>> GetByEquipeIdAsync(int equipeId)
        {
            return await _context.Employes
                .Where(e => e.EquipeId == equipeId)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Employe?> GetByIdWithEquipeAsync(string id)
        {
            return await _context.Employes
                .Include(e => e.Equipe)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task AddAsync(Employe employe)
        {
            await _context.Employes.AddAsync(employe);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Employe employe)
        {
            // Mettre à jour l'utilisateur ASP.NET
            var appUser = await _userManager.FindByIdAsync(employe.Id);
            if (appUser != null)
            {
                appUser.Nom = employe.Nom;
                appUser.Prenom = employe.Prenom;
                appUser.Email = employe.Email;
                appUser.UserName = employe.Email;
                appUser.Adresse = employe.Adresse;
                appUser.DateNaissance = employe.DateNaissance;

                var identityResult = await _userManager.UpdateAsync(appUser);
                if (!identityResult.Succeeded)
                {
                    // Tu peux logguer ou gérer l’erreur ici
                    throw new Exception("Échec de la mise à jour de l'utilisateur ASP.NET");
                }
            }

            // Mettre à jour l'entité Employe
            _context.Employes.Update(employe);
            await _context.SaveChangesAsync();
        }


        public async Task DeleteAsync(Employe employe)
        {
            // Supprimer l'utilisateur ApplicationUser associé
            var appUser = await _userManager.FindByIdAsync(employe.Id);
            if (appUser != null)
            {
                var identityResult = await _userManager.DeleteAsync(appUser);
                if (!identityResult.Succeeded)
                {
                    // Gère les erreurs si nécessaire
                    throw new Exception("Échec de la suppression de l'utilisateur ASP.NET");
                }
            }
            var evaluations = await _context.Evaluations.Where(e => e.EmployeId == employe.Id).ToListAsync();
            _context.Evaluations.RemoveRange(evaluations);

            var conges = await _context.Conges.Where(c => c.EmployeId == employe.Id).ToListAsync();
            _context.Conges.RemoveRange(conges);

            var inscriptions = await _context.Inscriptions.Where(i => i.EmployeId == employe.Id).ToListAsync();
            _context.Inscriptions.RemoveRange(inscriptions);

            var paies = await _context.Paies.Where(i => i.PersonneId == employe.Id).ToListAsync();
            _context.Paies.RemoveRange(paies);
            // Supprimer l'entité Employe
            _context.Employes.Remove(employe);
            await _context.SaveChangesAsync();
        }


        public async Task<bool> ExistsAsync(string id)
        {
            return await _context.Employes.AnyAsync(e => e.Id == id);
        }
        public async Task<bool> HasRoleAsync(string employeId, string roleName)
        {
            var user = await _userManager.FindByIdAsync(employeId);
            return user != null && await _userManager.IsInRoleAsync(user, roleName);
        }

        public async Task<Employe?> GetAdminAsync()
        {
            var adminUsers = await _userManager.GetUsersInRoleAsync("Admin");
            var adminUser = adminUsers.FirstOrDefault();
            if (adminUser == null) return null;

            return await _context.Employes.FirstOrDefaultAsync(e => e.Id == adminUser.Id);
        }

        public async Task<Employe?> GetManagerByEquipeIdAsync(int equipeId)
        {
            var managers = await _userManager.GetUsersInRoleAsync("Manager");
            var managerIds = managers.Select(m => m.Id).ToList();

            return await _context.Employes
                .FirstOrDefaultAsync(e => managerIds.Contains(e.Id) && e.EquipeId == equipeId);
        }
    }
}
