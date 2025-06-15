
using backend_projetdev.Application.DTOs;
using backend_projetdev.Domain.Entities;

namespace backend_projetdev.Application.Interfaces
{
    public interface IEmployeService
    {
        Task<Employe?> GetByIdAsync(string id);
        Task<bool> TransformerCandidatEnEmploye(TransformationEmployeDto model);
        Task<List<Employe>> GetAllAsync();
    }
}
