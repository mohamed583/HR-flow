using backend_projetdev.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backend_projetdev.Application.Interfaces
{
    public interface IPosteRepository
    {
        Task<List<Poste>> GetAllAsync();
        Task<Poste?> GetByIdAsync(int id);
        Task<List<Candidature>> GetCandidaturesByPosteIdAsync(int posteId);
        Task AddAsync(Poste poste);
        Task UpdateAsync(Poste poste);
        Task DeleteAsync(Poste poste);
        Task<bool> ExistsAsync(int id);
    }

}
