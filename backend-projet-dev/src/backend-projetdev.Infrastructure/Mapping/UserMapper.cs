using backend_projetdev.Domain.Entities;
using Infrastructure.Authentication;

namespace backend_projetdev.Infrastructure.Mapping
{
    public static class UserMapper
    {
        public static ApplicationUser ToApplicationUser(Candidat candidat)
        {
            return new ApplicationUser
            {
                Id = candidat.Id,
                UserName = candidat.Email,
                Email = candidat.Email,
                Nom = candidat.Nom,
                Prenom = candidat.Prenom
            };
        }

        public static Candidat ToCandidat(ApplicationUser user)
        {
            return new Candidat
            {
                Id = user.Id,
                Email = user.Email,
                Nom = user.Nom,
                Prenom = user.Prenom
            };
        }
    }

}
