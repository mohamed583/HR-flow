using backend_projetdev.Domain.Enums;

namespace backend_projetdev.Application.DTOs
{
    public class TransformationEmployeDto
    {
        public string CandidatureId { get; set; }
        public string CandidatId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Metier { get; set; }
        public decimal Salaire { get; set; }
        public string Adresse { get; set; }
        public DateTime DateNaissance { get; set; }
        public int EquipeId { get; set; }
        public StatutContractuel Contrat { get; set; }
        public bool EstManager { get; set; }
    }
}
