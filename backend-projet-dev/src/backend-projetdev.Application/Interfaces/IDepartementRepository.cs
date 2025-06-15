using backend_projetdev.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backend_projetdev.Application.Interfaces
{
    public interface IDepartementRepository
    {
        Task<List<Departement>> GetAllWithEquipesAsync();
        Task<Departement> GetByIdWithEquipesAsync(int id);
        Task<Departement> GetByIdAsync(int id);
        Task AddAsync(Departement departement);
        Task UpdateAsync(Departement departement);
        Task DeleteAsync(Departement departement);
        Task<bool> HasEquipesAsync(int departementId);
    }
}
