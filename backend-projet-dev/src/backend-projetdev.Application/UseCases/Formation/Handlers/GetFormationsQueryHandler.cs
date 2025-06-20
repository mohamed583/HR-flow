using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using backend_projetdev.Application.Common;
using backend_projetdev.Application.DTOs;
using backend_projetdev.Application.Interfaces;
using backend_projetdev.Application.UseCases.Formation.Queries;
using MediatR;

namespace backend_projetdev.Application.UseCases.Formation.Handlers
{
    public class GetFormationsQueryHandler : IRequestHandler<GetFormationsQuery, Result<List<FormationDto>>>
    {
        private readonly IFormationRepository _formationRepository;
        private readonly IMapper _mapper;

        public GetFormationsQueryHandler(IFormationRepository formationRepository, IMapper mapper)
        {
            _formationRepository = formationRepository;
            _mapper = mapper;
        }

        public async Task<Result<List<FormationDto>>> Handle(GetFormationsQuery request, CancellationToken cancellationToken)
        {
            var formations = await _formationRepository.GetAllAsync();
            var dtos = _mapper.Map<List<FormationDto>>(formations);
            return Result<List<FormationDto>>.SuccessResult(dtos);
        }
    }
}
