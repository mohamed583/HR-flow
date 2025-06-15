
using backend_projetdev.Domain.Enums;

namespace backend_projetdev.Application.DTOs
{
    public class PosteDto
    {
        public int Id { get; set; }
        public string Nom { get; set; } = default!;
        public string Description { get; set; }
        public StatutPoste StatutPoste { get; set; }
    }
}
