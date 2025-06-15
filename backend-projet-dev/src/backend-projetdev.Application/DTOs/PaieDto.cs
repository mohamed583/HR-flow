using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backend_projetdev.Application.DTOs
{
    public class PaieDto
    {
        public int Id { get; set; }
        public string PersonneId { get; set; }
        public string NomComplet { get; set; }
        public DateTime DatePaie { get; set; }
        public decimal Montant { get; set; }
        public string Description { get; set; }
        public string Avantages { get; set; }
        public string Retenues { get; set; }
    }
}
