using backend_projetdev.Application.Common;
using backend_projetdev.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backend_projetdev.Application.UseCases.Formation.Commands
{
    public class CreateFormationCommand : IRequest<Result<FormationDto>>
    {
        public string Titre { get; set; }
        public string Description { get; set; }
        public DateTime DateDebut { get; set; }
        public DateTime DateFin { get; set; }
        public string FormateurId { get; set; }
        public double Cout { get; set; }
    }
}
