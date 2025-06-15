using AutoMapper;
using backend_projetdev.Application.Common;
using backend_projetdev.Application.DTOs;
using backend_projetdev.Application.Interfaces;
using backend_projetdev.Application.UseCases.Inscription.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backend_projetdev.Application.UseCases.Inscription.Handlers
{
    public class GetFormationsDisponiblesHandler : IRequestHandler<GetFormationsDisponiblesQuery, Result<List<FormationDto>>>
    {
        private readonly IInscriptionRepository _repo;
        private readonly IMapper _mapper;

        public GetFormationsDisponiblesHandler(IInscriptionRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<Result<List<FormationDto>>> Handle(GetFormationsDisponiblesQuery request, CancellationToken cancellationToken)
        {
            var formations = await _repo.GetFormationsDisponiblesAsync(request.EmployeId);
            var result = _mapper.Map<List<FormationDto>>(formations);
            return Result<List<FormationDto>>.SuccessResult(result);
        }
    }
}
