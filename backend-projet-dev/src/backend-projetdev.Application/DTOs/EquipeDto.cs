using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backend_projetdev.Application.DTOs
{
    public class EquipeDto
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public int DepartementId { get; set; }
        public string DepartementNom { get; set; }
        public List<string> EmployeIds { get; set; }
    }

}
