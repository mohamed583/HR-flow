using backend_projetdev.Application.Common;
using backend_projetdev.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backend_projetdev.Application.UseCases.Entretien.Queries
{
    public class GetEntretienDetailsQuery : IRequest<Result<EntretienDetailsDto>>
    {
        public string EntretienId { get; set; }

    }
}
