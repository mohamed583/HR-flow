using backend_projetdev.Application.Common;
using backend_projetdev.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backend_projetdev.Application.UseCases.Paie.Commands
{
    public class CreatePaieCommand : IRequest<Result<PaieDto>>
    {
        public string PersonneId { get; set; }
        public DateTime DatePaie { get; set; }
        public decimal Montant { get; set; }
        public string Description { get; set; }
        public string Avantages { get; set; }
        public string Retenues { get; set; }
    }
}
