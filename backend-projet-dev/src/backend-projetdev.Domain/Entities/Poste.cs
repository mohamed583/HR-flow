using backend_projetdev.Domain.Enums;

namespace backend_projetdev.Domain.Entities
{
    public class Poste
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Description { get; set; }

        public StatutPoste StatutPoste { get; set; }

        public ICollection<Candidature> Candidatures { get; set; } = new List<Candidature>();
    }
}
