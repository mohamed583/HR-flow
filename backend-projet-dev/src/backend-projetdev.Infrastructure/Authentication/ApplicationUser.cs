using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Authentication
{
    public class ApplicationUser : IdentityUser
    {
        public string Nom { get; set; } = default!;
        public string Prenom { get; set; } = default!;
        public string? Sexe { get; set; } = default!;
        public DateTime? DateNaissance { get; set; }
        public string? Adresse { get; set; } = default!;
    }
}
