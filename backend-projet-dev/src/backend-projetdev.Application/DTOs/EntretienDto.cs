using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backend_projetdev.Application.DTOs
{
    public class EntretienDto
    {
        public string Id { get; set; } = string.Empty;
        public string CandidatureId { get; set; } = string.Empty;
        public string EmployeId { get; set; } = string.Empty;
        public string NewEmployeId { get; set; } = string.Empty;
        public DateTime DateEntretien { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}