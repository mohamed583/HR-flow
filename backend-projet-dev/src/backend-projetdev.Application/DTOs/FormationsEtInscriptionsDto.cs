using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backend_projetdev.Application.DTOs
{
    public class FormationsEtInscriptionsDto
    {
        public int Id { get; set; }
        public string Titre { get; set; }
        public string Description { get; set; }
        public string FormateurId { get; set; }

        public List<InscriptionDto> Inscriptions { get; set; } = new();
    }
}
