using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backend_projetdev.Application.DTOs
{
    public class EquipesOfDepartementDto
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public List<string> EmployeIds { get; set; }
    }
}
