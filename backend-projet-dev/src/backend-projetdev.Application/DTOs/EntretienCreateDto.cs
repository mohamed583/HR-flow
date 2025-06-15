using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backend_projetdev.Application.DTOs
{
    public class EntretienCreateDto
    {
        public string CandidatureId { get; set; } = string.Empty;
        public string EmployeId { get; set; } = string.Empty;
        public DateTime DateEntretien { get; set; }
    }
}