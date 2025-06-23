using backend_projetdev.Application.Common;
using backend_projetdev.Application.DTOs;
using backend_projetdev.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backend_projetdev.Application.Interfaces
{
    public interface IInscriptionRepository
    {
        Task<List<InscriptionDto>> GetInscriptionsByFormationAsync(int formationId);
        Task<InscriptionDto?> FindByIdAsync(int id);
        Task<List<InscriptionDto>> GetAllAsync();
        Task<bool> ApproveInscriptionAsync(int id);
        Task<bool> RejectInscriptionAsync(int id);
        Task<List<FormationDto>> GetFormationsDisponiblesAsync(string employeId);
        Task<Result> PostulerAsync(int formationId, string employeId);
        Task<List<InscriptionDto>> GetMesInscriptionsAsync(string employeId);
        Task<Result> SupprimerInscriptionAsync(int id);
        Task<List<FormationsEtInscriptionsDto>> GetFormationsWithInscriptionsByFormateurAsync(string formateurId);
    }
}