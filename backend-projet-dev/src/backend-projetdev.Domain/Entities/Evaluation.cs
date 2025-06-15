using backend_projetdev.Domain.Enums;

namespace backend_projetdev.Domain.Entities
{
    public class Evaluation
    {
        public int Id { get; set; }
        public string EmployeId { get; set; }
        public Employe Employe { get; set; }

        public string ResponsableId { get; set; }
        public Employe Responsable { get; set; }

        public DateTime DateEvaluation { get; set; }
        public double Note { get; set; }
        public string Description { get; set; }
        public string CommentairesEmploye { get; set; }
        public string CommentairesResponsable { get; set; }
        public string Objectifs { get; set; }
        public bool FinaliseParEmploye { get; set; }
        public bool FinaliseParManager { get; set; }

        public EstApprouve EstApprouve { get; set; }
    }
}