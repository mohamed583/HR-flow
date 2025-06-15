using backend_projetdev.Domain.Enums;

namespace backend_projetdev.Domain.Entities
{
    public class Inscription
    {
        public int Id { get; set; }
        public string EmployeId { get; set; }
        public Employe Employe { get; set; }

        public int FormationId { get; set; }
        public Formation Formation { get; set; }

        public DateTime DateInscription { get; set; }
        public Status StatusInscription { get; set; }
    }
}
