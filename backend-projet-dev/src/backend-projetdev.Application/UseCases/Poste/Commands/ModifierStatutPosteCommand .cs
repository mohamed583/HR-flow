using backend_projetdev.Application.Common;
using backend_projetdev.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backend_projetdev.Application.UseCases.Poste.Commands
{
    public class ModifierStatutPosteCommand : IRequest<Result>
    {
        public int Id { get; set; }
        public StatutPoste NouveauStatut { get; set; }
    }
}
