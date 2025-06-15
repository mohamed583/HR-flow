using backend_projetdev.Application.Interfaces;
using backend_projetdev.Domain.Entities;
using backend_projetdev.Domain.Enums;
using backend_projetdev.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace backend_projetdev.Infrastructure.Repositories
{
    public class EntretienRepository : IEntretienRepository
    {
        private readonly ApplicationDbContext _context;

        public EntretienRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Entretien?> GetByIdAsync(string id)
        {
            return await _context.Entretiens
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<List<Entretien>> GetByCandidatureIdAsync(string candidatureId)
        {
            return await _context.Entretiens
                .Where(e => e.CandidatureId == candidatureId)
                .ToListAsync();
        }

        public async Task<List<Entretien>> GetByEmployeIdAsync(string employeId)
        {
            return await _context.Entretiens
                .Where(e => e.EmployeId == employeId)
                .ToListAsync();
        }

        public async Task<List<Entretien>> GetNonFinalizedByEmployeIdAsync(string employeId)
        {
            return await _context.Entretiens
                .Where(e => e.EmployeId == employeId && string.IsNullOrEmpty(e.Commentaire))
                .ToListAsync();
        }

        public async Task<Entretien?> GetByIdWithDetailsAsync(string id)
        {
            return await _context.Entretiens
                .Include(e => e.Candidature)
                    .ThenInclude(c => c.Candidat)
                .Include(e => e.Employe)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<Entretien?> GetNonFinalisedByIdAndEmployeIdAsync(string entretienId, string employeId)
        {
            return await _context.Entretiens
                .Where(e => e.Id == entretienId && e.EmployeId == employeId && string.IsNullOrEmpty(e.Commentaire))
                .FirstOrDefaultAsync();
        }

        public async Task<List<Entretien>> GetNonFinalisedByEmployeIdAsync(string employeId)
        {
            return await _context.Entretiens
                .Where(e => e.EmployeId == employeId && string.IsNullOrEmpty(e.Commentaire) && e.Status != StatusEntretien.Finalise)
                .ToListAsync();
        }

        public async Task<List<Entretien>> GetWithCandidatureAndCandidatByEmployeIdAsync(string employeId)
        {
            return await _context.Entretiens
                .Include(e => e.Candidature)
                    .ThenInclude(c => c.Candidat)
                .Where(e => e.EmployeId == employeId)
                .ToListAsync();
        }

        public async Task AddAsync(Entretien entretien)
        {
            await _context.Entretiens.AddAsync(entretien);
            await _context.SaveChangesAsync();

        }

        public async Task UpdateAsync(Entretien entretien)
        {
            _context.Entretiens.Update(entretien);
            await Task.CompletedTask;
            await _context.SaveChangesAsync();

        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
            await _context.SaveChangesAsync();

        }
    }
}
