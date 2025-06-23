using backend_projetdev.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backend_projetdev.Application.DTOs
{
    public class EditEmployeDto
    {
        public string Id { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public decimal Salaire { get; set; }
        public string Metier { get; set; }
        public DateTime DateEmbauche { get; set; }
        public StatutContractuel Contrat { get; set; }
        public StatutEmploi Statut { get; set; }
        public string Adresse { get; set; }
        public DateTime DateNaissance { get; set; }
        public int? EquipeId { get; set; }
        public bool EstManager { get; set; }
    }

}
