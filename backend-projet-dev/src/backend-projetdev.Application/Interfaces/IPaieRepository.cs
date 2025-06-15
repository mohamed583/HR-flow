using backend_projetdev.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backend_projetdev.Application.Interfaces
{
    public interface IPaieRepository
    {
        Task<List<Paie>> GetAllAsync();
        Task<Paie?> GetByIdAsync(int id);
        Task<List<Paie>> GetByPersonneIdAsync(string personneId);
        Task AddAsync(Paie paie);
        Task AddRangeAsync(IEnumerable<Paie> paies);
    }
}
