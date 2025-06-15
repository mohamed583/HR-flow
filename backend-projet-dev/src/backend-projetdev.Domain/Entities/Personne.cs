using backend_projetdev.Domain.Enums;

namespace backend_projetdev.Domain.Entities
{
    public class Personne
    {
        public string Id { get; set; } // Guid ou string selon l’implémentation finale
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public ICollection<Paie> Paies { get; set; } = new List<Paie>();
    }

}
