using backend_projetdev.Application.Interfaces;
using backend_projetdev.Domain.Entities;
using backend_projetdev.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace backend_projetdev.Infrastructure.Repositories
{
    public class PosteRepository : IPosteRepository
    {
        private readonly ApplicationDbContext _context;

        public PosteRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Poste>> GetAllAsync()
        {
            return await _context.Postes.ToListAsync();
        }

        public async Task<Poste?> GetByIdAsync(int id)
        {
            return await _context.Postes.FindAsync(id);
        }

        public async Task<List<Candidature>> GetCandidaturesByPosteIdAsync(int posteId)
        {
            return await _context.Candidatures
                .Where(c => c.PosteId == posteId)
                .Include(c => c.Candidat)
                .ToListAsync();
        }

        public async Task AddAsync(Poste poste)
        {
            await _context.Postes.AddAsync(poste);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Poste poste)
        {
            _context.Postes.Update(poste);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Poste poste)
        {
            _context.Postes.Remove(poste);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Postes.AnyAsync(p => p.Id == id);
        }
    }
}
