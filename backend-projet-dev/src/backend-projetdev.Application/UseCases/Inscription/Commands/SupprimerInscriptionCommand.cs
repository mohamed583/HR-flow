using backend_projetdev.Application.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backend_projetdev.Application.UseCases.Inscription.Commands
{
    public class SupprimerInscriptionCommand : IRequest<Result>
    {
        public int InscriptionId { get; set; }
    }
}
