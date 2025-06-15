using backend_projetdev.Application.Interfaces;
using backend_projetdev.Domain.Entities;
using backend_projetdev.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backend_projetdev.Infrastructure.Repositories
{
    public class CandidatRepository : ICandidatRepository
    {
        private readonly ApplicationDbContext _context;

        public CandidatRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Candidat?> GetByIdAsync(string id)
        {
            return await _context.Candidats.FindAsync(id);
        }

        public async Task<Candidat?> GetByEmailAsync(string email)
        {
            return await _context.Candidats.FirstOrDefaultAsync(c => c.Email == email);
        }

        public async Task<List<Candidat>> GetAllAsync()
        {
            return await _context.Candidats.ToListAsync();
        }

        public async Task AddAsync(Candidat candidat)
        {
            await _context.Candidats.AddAsync(candidat);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Candidat candidat)
        {
            _context.Candidats.Update(candidat);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Candidat candidat)
        {
            _context.Candidats.Remove(candidat);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(string id)
        {
            return await _context.Candidats.AnyAsync(c => c.Id == id);
        }
    }
}