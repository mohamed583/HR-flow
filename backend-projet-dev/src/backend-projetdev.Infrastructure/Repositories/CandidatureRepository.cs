using backend_projetdev.Application.Interfaces;
using backend_projetdev.Domain.Entities;
using backend_projetdev.Domain.Enums;
using backend_projetdev.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace backend_projetdev.Infrastructure.Repositories
{
    public class CandidatureRepository : ICandidatureRepository
    {
        private readonly ApplicationDbContext _context;

        public CandidatureRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Candidature candidature)
        {
            await _context.Candidatures.AddAsync(candidature);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(string id, string userId)
        {
            var candidature = await _context.Candidatures
                .FirstOrDefaultAsync(c => c.Id == id && c.CandidatId == userId);

            if (candidature == null)
                return false;

            _context.Candidatures.Remove(candidature);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Candidature?> GetByIdAsync(string id)
        {
            return await _context.Candidatures
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Candidature>> GetByPosteIdAsync(int posteId)
        {
            return await _context.Candidatures
                .Where(c => c.PosteId == posteId)
                .Include(c => c.Poste)
                .Include(c => c.Candidat)
                .ToListAsync();
        }

        public async Task<IEnumerable<Candidature>> GetByCandidatIdAsync(string candidatId)
        {
            return await _context.Candidatures
                .Where(c => c.CandidatId == candidatId)
                .Include(c => c.Poste)
                .Include(c => c.Candidat)
                .ToListAsync();
        }

        public async Task<Candidature?> GetDetailsAsync(string id, string currentUserId, bool isAdmin)
        {
            return await _context.Candidatures
                .Include(c => c.Poste)
                .Include(c => c.Candidat)
                .FirstOrDefaultAsync(c =>
                    c.Id == id &&
                    (isAdmin || c.CandidatId == currentUserId));
        }

        public async Task<bool> UpdateStatusAsync(string id, Status newStatus)
        {
            var candidature = await _context.Candidatures.FindAsync(id);
            if (candidature == null)
                return false;

            candidature.Status = newStatus;
            _context.Candidatures.Update(candidature);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<string?> GetCvPathByIdAsync(string id)
        {
            return await _context.Candidatures
                .Where(c => c.Id == id)
                .Select(c => c.CVPath)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> HasAlreadyApplied(string candidatId, int posteId)
        {
            return await _context.Candidatures
                .AnyAsync(c => c.CandidatId == candidatId && c.PosteId == posteId);
        }
    }
}
