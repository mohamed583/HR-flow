using backend_projetdev.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backend_projetdev.Application.DTOs
{
    public class ManagePosteDto
    {
        public string Nom { get; set; }
        public string Description { get; set; }
        public StatutPoste StatutPoste { get; set; }
    }
}
