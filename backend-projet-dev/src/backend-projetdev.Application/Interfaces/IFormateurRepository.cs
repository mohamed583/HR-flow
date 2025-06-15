using backend_projetdev.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backend_projetdev.Application.Interfaces
{
    public interface IFormateurRepository
    {
        Task<List<Formateur>> GetAllAsync();
        Task<Formateur?> GetByIdAsync(string id);
        Task<bool> CreateAsync(Formateur formateur, string password);
        Task AddAsync(Formateur formateur);
        Task<bool> UpdateAsync(Formateur formateur);
        Task<bool> DeleteAsync(Formateur formateur);
    }
}
