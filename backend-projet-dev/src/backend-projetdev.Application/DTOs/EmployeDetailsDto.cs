using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using backend_projetdev.Domain.Entities;
using backend_projetdev.Domain.Enums;

namespace backend_projetdev.Application.DTOs
{
    public class EmployeDetailsDto
    {
        public string Id { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Metier { get; set; }
        public decimal Salaire { get; set; }
        public DateTime DateEmbauche { get; set; }
        public StatutContractuel Contrat { get; set; }
        public StatutEmploi Statut { get; set; }
        public int? EquipeId { get; set; }
        public string EquipeNom { get; set; }
        public string Adresse { get; set; }
        public DateTime DateNaissance { get; set; }

    }

}
