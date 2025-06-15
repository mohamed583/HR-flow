using backend_projetdev.Application.Interfaces;
using backend_projetdev.Domain.Entities;
using Infrastructure.Authentication;
using Microsoft.AspNetCore.Identity;

namespace backend_projetdev.Infrastructure.Services
{
    public class PersonneService : IPersonneService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public PersonneService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Personne?> GetByIdAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
                return null;

            // Mapper ApplicationUser → Personne (manuellement ou via AutoMapper si tu préfères)
            return new Personne
            {
                Id = user.Id,
                Nom = user.Nom,
                Prenom = user.Prenom
                // mappe les autres propriétés si nécessaire
            };
        }
    }
}
