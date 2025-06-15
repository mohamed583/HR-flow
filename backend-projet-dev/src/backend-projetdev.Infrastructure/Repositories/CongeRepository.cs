using backend_projetdev.Application.Interfaces;
using backend_projetdev.Domain.Entities;
using backend_projetdev.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace backend_projetdev.Infrastructure.Repositories
{
    public class CongeRepository : ICongeRepository
    {
        private readonly ApplicationDbContext _context;

        public CongeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Conge>> GetAllAsync()
        {
            return await _context.Conges
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<List<Conge>> GetByEmployeIdAsync(string employeId)
        {
            return await _context.Conges
                .Where(c => c.EmployeId == employeId)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<List<Conge>> GetByEquipeIdAsync(int equipeId)
        {
            return await _context.Conges
                .Where(c => c.Employe.EquipeId == equipeId)
                .Include(c => c.Employe)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Conge?> GetByIdAsync(int id)
        {
            return await _context.Conges
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task AddAsync(Conge conge)
        {
            await _context.Conges.AddAsync(conge);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Conge conge)
        {
            _context.Conges.Update(conge);
            await Task.CompletedTask;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Conge conge)
        {
            _context.Conges.Remove(conge);
            await Task.CompletedTask;
            await _context.SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
