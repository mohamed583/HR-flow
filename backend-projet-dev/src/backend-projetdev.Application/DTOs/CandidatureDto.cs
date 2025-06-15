using backend_projetdev.Domain.Enums;

namespace backend_projetdev.Application.DTOs
{
    public class CandidatureDto
    {
        public string Id { get; set; }
        public string CandidatId { get; set; }
        public int PosteId { get; set; }
        public string CVPath { get; set; }
        public Status Status { get; set; }
        public PosteDto Poste { get; set; }
        public CandidatDto Candidat { get; set; }
    }
}
