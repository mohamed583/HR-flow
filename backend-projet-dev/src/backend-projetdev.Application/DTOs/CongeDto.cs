using backend_projetdev.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backend_projetdev.Application.DTOs
{
    public class CongeDto
    {
        public int Id { get; set; }
        public DateTime DateDebut { get; set; }
        public DateTime DateFin { get; set; }
        public Status StatusConge { get; set; }
        public string Type { get; set; }
        public string EmployeId { get; set; } = string.Empty;
    }
}
