using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backend_projetdev.Application.DTOs
{
    public class RegisterFormateurDto
    {
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Email { get; set; }
        public string Domaine { get; set; }
        public string Description { get; set; }
        public decimal Salaire { get; set; }
        public string Password { get; set; }
    }
}