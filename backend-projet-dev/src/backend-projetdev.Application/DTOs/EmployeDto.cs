using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backend_projetdev.Application.DTOs
{
    public class EmployeDto
    {
        public string Id { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Metier { get; set; }
        public string Statut { get; set; }
        public int? EquipeId { get; set; }
    }
}
