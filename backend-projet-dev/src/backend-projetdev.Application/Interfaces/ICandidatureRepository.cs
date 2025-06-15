using backend_projetdev.Domain.Entities;
using backend_projetdev.Domain.Enums;

namespace backend_projetdev.Application.Interfaces
{
    public interface ICandidatureRepository
    {
        Task<IEnumerable<Candidature>> GetByPosteIdAsync(int posteId);
        Task<IEnumerable<Candidature>> GetByCandidatIdAsync(string candidatId);
        Task<Candidature?> GetDetailsAsync(string id, string currentUserId, bool isAdmin);
        Task<Candidature?> GetByIdAsync(string id);
        Task<bool> UpdateStatusAsync(string id, Status newStatus);
        Task<bool> DeleteAsync(string id, string userId);
        Task<string?> GetCvPathByIdAsync(string id);
        Task<bool> HasAlreadyApplied(string candidatId, int posteId);
        Task AddAsync(Candidature candidature);
    }
}
