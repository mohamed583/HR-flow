
namespace backend_projetdev.Application.DTOs
{
    public class EvaluationDto
    {
        public int Id { get; set; }
        public string EmployeId { get; set; }

        public string ResponsableId { get; set; }

        public string CommentairesEmploye { get; set; }
        public string CommentairesResponsable { get; set; }

        public double Note { get; set; }
        public bool FinaliseParEmploye { get; set; }
        public bool FinaliseParManager { get; set; }

        public string EstApprouve { get; set; }
        public DateTime DateEvaluation { get; set; }
        public string Description { get; set; }
        public string Objectifs { get; set; }
    }
}