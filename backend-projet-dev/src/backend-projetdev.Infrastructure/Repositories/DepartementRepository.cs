using backend_projetdev.Application.Interfaces;
using backend_projetdev.Domain.Entities;
using backend_projetdev.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace backend_projetdev.Infrastructure.Repositories
{
    public class DepartementRepository : IDepartementRepository
    {
        private readonly ApplicationDbContext _context;

        public DepartementRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Departement>> GetAllWithEquipesAsync()
        {
            return await _context.Departements
                .Include(d => d.Equipes)
                .ThenInclude(e => e.Employes)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Departement> GetByIdWithEquipesAsync(int id)
        {
            return await _context.Departements
                .Include(d => d.Equipes)
                .ThenInclude(e => e.Employes)
                .FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<Departement> GetByIdAsync(int id)
        {
            return await _context.Departements
                .FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task AddAsync(Departement departement)
        {
            await _context.Departements.AddAsync(departement);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Departement departement)
        {
            _context.Departements.Update(departement);
            await Task.CompletedTask;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Departement departement)
        {
            _context.Departements.Remove(departement);
            await Task.CompletedTask;
            await _context.SaveChangesAsync();
        }

        public async Task<bool> HasEquipesAsync(int departementId)
        {
            return await _context.Equipes
                .AnyAsync(e => e.DepartementId == departementId);
        }
    }
}
