using backend_projetdev.Application.Common;
using backend_projetdev.Application.DTOs;
using backend_projetdev.Application.Interfaces;
using backend_projetdev.Domain.Entities;
using backend_projetdev.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace backend_projetdev.Infrastructure.Repositories
{
    public class InscriptionRepository : IInscriptionRepository
    {
        private readonly ApplicationDbContext _context;

        public InscriptionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<InscriptionDto>> GetInscriptionsByFormationAsync(int formationId)
        {
            return await _context.Inscriptions
                .Where(i => i.FormationId == formationId)
                .Include(i => i.Employe)
                .Select(i => new InscriptionDto
                {
                    Id = i.Id,
                    EmployeId = i.EmployeId,
                    FormationId = i.FormationId,
                    Statut = i.StatusInscription.ToString(),
                    NomEmploye = i.Employe.Nom + " " + i.Employe.Prenom
                })
                .ToListAsync();
        }

        public async Task<InscriptionDto?> FindByIdAsync(int id)
        {
            return await _context.Inscriptions
                .Include(i => i.Employe)
                .Where(i => i.Id == id)
                .Select(i => new InscriptionDto
                {
                    Id = i.Id,
                    EmployeId = i.EmployeId,
                    FormationId = i.FormationId,
                    Statut = i.StatusInscription.ToString(),
                    NomEmploye = i.Employe.Nom + " " + i.Employe.Prenom
                })
                .FirstOrDefaultAsync();
        }

        public async Task<bool> ApproveInscriptionAsync(int id)
        {
            var inscription = await _context.Inscriptions.FindAsync(id);
            if (inscription == null) return false;

            inscription.StatusInscription = Domain.Enums.Status.Approuve;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RejectInscriptionAsync(int id)
        {
            var inscription = await _context.Inscriptions.FindAsync(id);
            if (inscription == null) return false;

            inscription.StatusInscription = Domain.Enums.Status.Rejete;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<FormationDto>> GetFormationsDisponiblesAsync(string employeId)
        {
            var formationIdsInscrites = await _context.Inscriptions
                .Where(i => i.EmployeId == employeId)
                .Select(i => i.FormationId)
                .ToListAsync();

            return await _context.Formations
                .Where(f => !formationIdsInscrites.Contains(f.Id))
                .Select(f => new FormationDto
                {
                    Id = f.Id,
                    Titre = f.Titre,
                    Description = f.Description,
                    FormateurId = f.FormateurId
                })
                .ToListAsync();
        }

        public async Task<Result> PostulerAsync(int formationId, string employeId)
        {
            var alreadyExists = await _context.Inscriptions
                .AnyAsync(i => i.FormationId == formationId && i.EmployeId == employeId);

            if (alreadyExists)
                return Result.Failure("Vous êtes déjà inscrit à cette formation.");

            var inscription = new Inscription
            {
                FormationId = formationId,
                EmployeId = employeId,
                StatusInscription = Domain.Enums.Status.EnCours
            };

            await _context.Inscriptions.AddAsync(inscription);
            await _context.SaveChangesAsync();
            return Result.SuccessResult();
        }

        public async Task<List<InscriptionDto>> GetMesInscriptionsAsync(string employeId)
        {
            return await _context.Inscriptions
                .Include(i => i.Formation)
                .Include(i => i.Employe)
                .Where(i => i.EmployeId == employeId)
                .Select(i => new InscriptionDto
                {
                    Id = i.Id,
                    FormationId = i.FormationId,
                    Statut = i.StatusInscription.ToString(),
                    EmployeId = employeId,
                    NomEmploye = i.Employe.Nom + " " + i.Employe.Prenom
                })
                .ToListAsync();
        }

        public async Task<Result> SupprimerInscriptionAsync(int id)
        {
            var inscription = await _context.Inscriptions
                .FirstOrDefaultAsync(i => i.Id == id);

            if (inscription == null)
                return Result.Failure("Inscription introuvable ou non autorisée.");

            _context.Inscriptions.Remove(inscription);
            await _context.SaveChangesAsync();
            return Result.SuccessResult();
        }

        public async Task<List<FormationsEtInscriptionsDto>> GetFormationsWithInscriptionsByFormateurAsync(string formateurId)
        {
            return await _context.Formations
        .Where(f => f.FormateurId == formateurId)
        .Include(f => f.Inscriptions)
            .ThenInclude(i => i.Employe)
        .Select(f => new FormationsEtInscriptionsDto
        {
            Id = f.Id,
            Titre = f.Titre,
            Description = f.Description,
            FormateurId = f.FormateurId,
            Inscriptions = f.Inscriptions
                .Where(i => i.StatusInscription == Domain.Enums.Status.Approuve)
                .Select(i => new InscriptionDto
                {
                    Id = i.Id,
                    EmployeId = i.Employe.Id,
                    NomEmploye = i.Employe.Nom + " " + i.Employe.Prenom,
                    FormationId = i.FormationId,
                    DateInscription = i.DateInscription,
                    Statut = i.StatusInscription.ToString()
                }).ToList()
        })
        .ToListAsync();
        }
    }
}
