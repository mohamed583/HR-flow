using backend_projetdev.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backend_projetdev.Application.Interfaces
{
    public interface IEntretienRepository
    {
        Task<Entretien?> GetByIdAsync(string id);
        Task<List<Entretien>> GetAllAsync();
        Task<List<Entretien>> GetByCandidatureIdAsync(string candidatureId);
        Task<List<Entretien>> GetByEmployeIdAsync(string employeId);
        Task<List<Entretien>> GetNonFinalizedByEmployeIdAsync(string employeId);
        Task<Entretien?> GetByIdWithDetailsAsync(string id);
        Task<Entretien?> GetNonFinalisedByIdAndEmployeIdAsync(string entretienId, string employeId);
        Task<List<Entretien>> GetNonFinalisedByEmployeIdAsync(string employeId);
        Task<List<Entretien>> GetWithCandidatureAndCandidatByEmployeIdAsync(string employeId);
        Task AddAsync(Entretien entretien);
        Task UpdateAsync(Entretien entretien);
        Task SaveChangesAsync();
    }
}