using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backend_projetdev.Application.DTOs
{
    public class PersonneDto
    {
        public string Id { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Discriminator { get; set; }
        public IList<string> Roles { get; set; } = new List<string>();
    }
}