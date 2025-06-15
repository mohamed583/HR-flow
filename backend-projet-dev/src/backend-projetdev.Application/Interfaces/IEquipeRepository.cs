using backend_projetdev.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backend_projetdev.Application.Interfaces
{
    public interface IEquipeRepository
    {
        Task<Equipe?> GetByIdAsync(int id);
        Task<List<Employe>> GetEmployesByEquipeAsync(int equipeId);
        Task<List<Equipe>> GetByDepartementAsync(int departementId);
        Task<Equipe> GetWithEmployesByIdAsync(int id);
        Task<Equipe> GetWithDetailsByIdAsync(int id);
        Task AddAsync(Equipe equipe);
        Task UpdateAsync(Equipe equipe);
        Task DeleteAsync(Equipe equipe);
    }
}
