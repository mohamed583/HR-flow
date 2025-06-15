using backend_projetdev.Domain.Enums;

namespace backend_projetdev.Domain.Entities
{
    public class Employe : Personne
    {
        public int EquipeId { get; set; }
        public Equipe Equipe { get; set; }
        public decimal Salaire { get; set; }
        public string Metier { get; set; }
        public string Adresse { get; set; }
        public DateTime DateEmbauche { get; set; }
        public DateTime DateNaissance { get; set; }
        public StatutContractuel Contrat { get; set; }
        public StatutEmploi Statut { get; set; }

        public ICollection<Inscription> Inscriptions { get; set; } = new List<Inscription>();
        public ICollection<Evaluation> Evaluations { get; set; } = new List<Evaluation>();
        public ICollection<Conge> Conges { get; set; } = new List<Conge>();
    }
}
