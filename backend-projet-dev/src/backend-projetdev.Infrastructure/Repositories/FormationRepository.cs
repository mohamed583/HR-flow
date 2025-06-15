using backend_projetdev.Application.Interfaces;
using backend_projetdev.Domain.Entities;
using backend_projetdev.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace backend_projetdev.Infrastructure.Repositories
{
    public class FormationRepository : IFormationRepository
    {
        private readonly ApplicationDbContext _context;

        public FormationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Formation?> GetByIdAsync(int id)
        {
            return await _context.Formations.FindAsync(id);
        }

        public async Task<Formation?> GetByIdWithFormateurAsync(int id)
        {
            return await _context.Formations
                .Include(f => f.Formateur)
                .FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task<List<Formation>> GetByFormateurIdAsync(string formateurId)
        {
            return await _context.Formations
                .Where(f => f.FormateurId == formateurId)
                .ToListAsync();
        }

        public async Task<List<Formation>> GetAllAsync()
        {
            return await _context.Formations.ToListAsync();
        }

        public async Task AddAsync(Formation formation)
        {
            await _context.Formations.AddAsync(formation);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Formation formation)
        {
            _context.Formations.Update(formation);
            await Task.CompletedTask;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Formation formation)
        {
            _context.Formations.Remove(formation);
            await Task.CompletedTask;
            await _context.SaveChangesAsync();
        }
    }
}
