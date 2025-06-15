using backend_projetdev.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backend_projetdev.Application.Interfaces
{
    public interface IEmployeRepository
    {
        Task<Employe?> GetByIdAsync(string id);
        Task<Employe?> GetByEmailAsync(string email);
        Task<List<Employe>> GetAllAsync();
        Task<List<Employe>> GetByEquipeIdAsync(int equipeId);
        Task<Employe?> GetByIdWithEquipeAsync(string id);
        Task AddAsync(Employe employe);
        Task UpdateAsync(Employe employe);
        Task DeleteAsync(Employe employe);
        Task<bool> ExistsAsync(string id);
        Task<bool> HasRoleAsync(string employeId, string roleName);
        Task<Employe?> GetAdminAsync();
        Task<Employe?> GetManagerByEquipeIdAsync(int equipeId);
    }
}