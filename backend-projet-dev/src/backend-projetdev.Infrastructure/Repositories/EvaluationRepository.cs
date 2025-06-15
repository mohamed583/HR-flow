using backend_projetdev.Application.Interfaces;
using backend_projetdev.Domain.Entities;
using backend_projetdev.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace backend_projetdev.Infrastructure.Repositories
{
    public class EvaluationRepository : IEvaluationRepository
    {
        private readonly ApplicationDbContext _context;

        public EvaluationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Evaluation?> GetByIdAsync(int id)
        {
            return await _context.Evaluations.FindAsync(id);
        }

        public async Task<List<Evaluation>> GetAllAsync()
        {
            return await _context.Evaluations.ToListAsync();
        }

        public async Task<List<Evaluation>> GetByEmployeIdAsync(string employeId)
        {
            return await _context.Evaluations
                .Where(e => e.EmployeId == employeId)
                .ToListAsync();
        }

        public async Task<List<Evaluation>> GetByResponsableIdAsync(string responsableId)
        {
            return await _context.Evaluations
                .Where(e => e.ResponsableId == responsableId)
                .ToListAsync();
        }

        public async Task AddAsync(Evaluation evaluation)
        {
            await _context.Evaluations.AddAsync(evaluation);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Evaluation evaluation)
        {
            _context.Evaluations.Update(evaluation);
            await Task.CompletedTask;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Evaluation evaluation)
        {
            _context.Evaluations.Remove(evaluation);
            await Task.CompletedTask;
            await _context.SaveChangesAsync();
        }

        public async Task<Evaluation?> GetByIdWithDetailsAsync(int id)
        {
            return await _context.Evaluations
                .Include(e => e.Employe)
                .Include(e => e.Responsable)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<List<Evaluation>> GetAllWithDetailsAsync()
        {
            return await _context.Evaluations
                .Include(e => e.Employe)
                .Include(e => e.Responsable)
                .ToListAsync();
        }

        public async Task<List<Evaluation>> GetByManagerEquipeAsync(string managerId)
        {
            return await _context.Evaluations
                .Include(e => e.Employe)
                .Include(e => e.Responsable)
                .Where(e => e.Employe != null && e.Employe.Equipe != null)
                .ToListAsync();
        }
    }
}
