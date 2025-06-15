using backend_projetdev.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backend_projetdev.Application.Interfaces
{
    public interface IEvaluationRepository
    {
        Task<Evaluation?> GetByIdAsync(int id);
        Task<List<Evaluation>> GetAllAsync();
        Task<List<Evaluation>> GetByEmployeIdAsync(string employeId);
        Task<List<Evaluation>> GetByResponsableIdAsync(string responsableId);
        Task AddAsync(Evaluation evaluation);
        Task UpdateAsync(Evaluation evaluation);
        Task DeleteAsync(Evaluation evaluation);
        Task<Evaluation?> GetByIdWithDetailsAsync(int id);
        Task<List<Evaluation>> GetAllWithDetailsAsync();
        Task<List<Evaluation>> GetByManagerEquipeAsync(string managerId);
    }
}