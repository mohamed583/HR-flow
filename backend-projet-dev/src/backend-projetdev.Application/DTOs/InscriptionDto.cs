using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backend_projetdev.Application.DTOs
{
    public class InscriptionDto
    {
        public int Id { get; set; }

        public string EmployeId { get; set; } = default!;
        public string NomEmploye { get; set; } = default!;

        public int FormationId { get; set; }

        public DateTime DateInscription { get; set; }
        public string Statut { get; set; } = default!;
    }
}
