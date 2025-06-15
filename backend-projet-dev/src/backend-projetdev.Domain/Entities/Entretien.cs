using backend_projetdev.Domain.Enums;

namespace backend_projetdev.Domain.Entities
{
    public class Entretien
    {
        public string Id { get; set; }
        public string CandidatureId { get; set; }
        public Candidature Candidature { get; set; }

        public string EmployeId { get; set; }
        public Employe Employe { get; set; }

        public DateTime DateEntretien { get; set; }
        public StatusEntretien Status { get; set; }
        public string Commentaire { get; set; }
    }
}
