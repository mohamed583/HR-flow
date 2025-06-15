using backend_projetdev.Application.Interfaces;
using backend_projetdev.Domain.Entities;
using backend_projetdev.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace backend_projetdev.Infrastructure.Repositories
{
    public class PaieRepository : IPaieRepository
    {
        private readonly ApplicationDbContext _context;

        public PaieRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Paie>> GetAllAsync()
        {
            return await _context.Paies
                .Include(p => p.Personne)
                .ToListAsync();
        }

        public async Task<Paie?> GetByIdAsync(int id)
        {
            return await _context.Paies
                .Include(p => p.Personne)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<Paie>> GetByPersonneIdAsync(string personneId)
        {
            return await _context.Paies
                .Where(p => p.PersonneId == personneId)
                .Include(p => p.Personne)
                .ToListAsync();
        }

        public async Task AddAsync(Paie paie)
        {
            await _context.Paies.AddAsync(paie);
            await _context.SaveChangesAsync();
        }

        public async Task AddRangeAsync(IEnumerable<Paie> paies)
        {
            await _context.Paies.AddRangeAsync(paies);
            await _context.SaveChangesAsync();
        }
    }
}
