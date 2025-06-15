using backend_projetdev.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backend_projetdev.Application.Interfaces
{
    public interface IFormationRepository
    {
        Task<Formation?> GetByIdAsync(int id);
        Task<Formation?> GetByIdWithFormateurAsync(int id);
        Task<List<Formation>> GetByFormateurIdAsync(string formateurId);
        Task<List<Formation>> GetAllAsync();
        Task AddAsync(Formation formation);
        Task UpdateAsync(Formation formation);
        Task DeleteAsync(Formation formation);
    }

}
