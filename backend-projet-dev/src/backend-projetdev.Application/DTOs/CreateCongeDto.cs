using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backend_projetdev.Application.DTOs
{
    public class CreateCongeDto
    {
        public DateTime DateDebut { get; set; }
        public DateTime DateFin { get; set; }
        public string Type { get; set; }
    }
}
