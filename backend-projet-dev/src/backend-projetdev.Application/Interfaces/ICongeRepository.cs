using backend_projetdev.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backend_projetdev.Application.Interfaces
{
    public interface ICongeRepository
    {
        Task<List<Conge>> GetAllAsync();
        Task<List<Conge>> GetByEmployeIdAsync(string employeId);
        Task<List<Conge>> GetByEquipeIdAsync(int equipeId);
        Task<Conge?> GetByIdAsync(int id);
        Task AddAsync(Conge conge);
        Task UpdateAsync(Conge conge);
        Task DeleteAsync(Conge conge);
        Task SaveChangesAsync();
    }
}
