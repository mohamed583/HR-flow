using backend_projetdev.Application.Common;
using backend_projetdev.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backend_projetdev.Application.UseCases.Formation.Queries
{
    public class GetFormationsByFormateurQuery : IRequest<Result<List<FormationDto>>>
    {
        public string FormateurId { get; set; }

        public GetFormationsByFormateurQuery(string formateurId)
        {
            FormateurId = formateurId;
        }
    }

}
