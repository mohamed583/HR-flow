using Azure.Core;
using backend_projetdev.Application.DTOs;
using backend_projetdev.Application.Interfaces;
using backend_projetdev.Domain.Entities;
using Infrastructure.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace backend_projetdev.Infrastructure.Services
{
    public class EmployeService : IEmployeService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmployeRepository _employeRepository;
        private readonly ICandidatRepository _candidatRepository;
        private readonly IEntretienRepository _entretienRepository;

        public EmployeService(
            UserManager<ApplicationUser> userManager,
            IEmployeRepository employeRepository,
            IEntretienRepository entretienRepository,
            ICandidatRepository candidatRepository)
        {
            _userManager = userManager;
            _employeRepository = employeRepository;
            _candidatRepository = candidatRepository;
            _entretienRepository = entretienRepository;
        }

        public async Task<Employe?> GetByIdAsync(string id)
        {
            return await _employeRepository.GetByIdAsync(id);
        }

        public async Task<List<Employe>> GetAllAsync()
        {
            return await _employeRepository.GetAllAsync();
        }

        public async Task<bool> TransformerCandidatEnEmploye(TransformationEmployeDto model)
        {
            
            var candidat = await _candidatRepository.GetByIdAsync(model.CandidatId);
            
            if (candidat == null)
            {
                
                return false;
            }
            var existingUser = await _userManager.FindByEmailAsync(model.Email);
            
            if (existingUser != null)
            {
                
                return false;
            }
            var employe = new Employe
            {
                Id = Guid.NewGuid().ToString(),
                Nom = candidat.Nom,
                Prenom = candidat.Prenom,
                Email = model.Email,
                Metier= model.Metier,
                Salaire =model.Salaire,
                Contrat = model.Contrat,
                UserName = model.Email,
                Adresse = model.Adresse,
                DateNaissance = model.DateNaissance,
                EquipeId = model.EquipeId,
                DateEmbauche = DateTime.Today,
                Statut = Domain.Enums.StatutEmploi.Inactif
            };

            var appUser = new ApplicationUser
            {
                Id = employe.Id,
                UserName = model.Email,
                Email = model.Email,
                Nom = candidat.Nom,
                Prenom = candidat.Prenom,
                Adresse = model.Adresse,
                DateNaissance = model.DateNaissance
            };

            var result = await _userManager.CreateAsync(appUser, model.Password);
            if (!result.Succeeded)
                return false;

            await _userManager.AddToRoleAsync(appUser, "Employe");
            if (model.EstManager)
            {
                await _userManager.AddToRoleAsync(appUser, "Manager");
            }
            await _employeRepository.AddAsync(employe);
            var entretiens = await _entretienRepository.GetByCandidatureIdAsync(model.CandidatureId);
            foreach (var entretien in entretiens)
            {
                entretien.NewEmployeId = employe.Id;
                await _entretienRepository.UpdateAsync(entretien);
            }
            return true;
        }
    }
}
