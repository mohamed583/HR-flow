using backend_projetdev.Application.Interfaces;
using backend_projetdev.Domain.Entities;
using backend_projetdev.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace backend_projetdev.Infrastructure.Repositories
{
    public class EquipeRepository : IEquipeRepository
    {
        private readonly ApplicationDbContext _context;

        public EquipeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Equipe?> GetByIdAsync(int id)
        {
            return await _context.Equipes
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<List<Employe>> GetEmployesByEquipeAsync(int equipeId)
        {
            return await _context.Employes
                .Where(emp => emp.EquipeId == equipeId)
                .ToListAsync();
        }

        public async Task<List<Equipe>> GetByDepartementAsync(int departementId)
        {
            return await _context.Equipes
                .Where(e => e.DepartementId == departementId)
                .ToListAsync();
        }

        public async Task<Equipe> GetWithEmployesByIdAsync(int id)
        {
            return await _context.Equipes
                .Include(e => e.Employes)
                .FirstAsync(e => e.Id == id);
        }

        public async Task<Equipe> GetWithDetailsByIdAsync(int id)
        {
            return await _context.Equipes
                .Include(e => e.Employes)
                .Include(e => e.Departement)
                .FirstAsync(e => e.Id == id);
        }

        public async Task AddAsync(Equipe equipe)
        {
            await _context.Equipes.AddAsync(equipe);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Equipe equipe)
        {
            _context.Equipes.Update(equipe);
            await Task.CompletedTask;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Equipe equipe)
        {
            _context.Equipes.Remove(equipe);
            await Task.CompletedTask;
            await _context.SaveChangesAsync();
        }
    }
}
