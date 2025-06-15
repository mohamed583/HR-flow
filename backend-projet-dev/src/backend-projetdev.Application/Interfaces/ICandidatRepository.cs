using backend_projetdev.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backend_projetdev.Application.Interfaces
{
    public interface ICandidatRepository
    {
        Task<Candidat?> GetByIdAsync(string id);
        Task<Candidat?> GetByEmailAsync(string email);
        Task<List<Candidat>> GetAllAsync();
        Task AddAsync(Candidat candidat);
        Task UpdateAsync(Candidat candidat);
        Task DeleteAsync(Candidat candidat);
        Task<bool> ExistsAsync(string id);
    }
}