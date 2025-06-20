using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using backend_projetdev.Application.Common;
using backend_projetdev.Application.DTOs;
using MediatR;

namespace backend_projetdev.Application.UseCases.Formation.Queries
{
    public class GetFormationsQuery : IRequest<Result<List<FormationDto>>>
    {
        
    }
}
