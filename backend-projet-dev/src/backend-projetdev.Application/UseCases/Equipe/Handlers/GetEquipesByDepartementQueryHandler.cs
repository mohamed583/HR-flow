using AutoMapper;
using backend_projetdev.Application.Common;
using backend_projetdev.Application.DTOs;
using backend_projetdev.Application.Interfaces;
using backend_projetdev.Application.UseCases.Equipe.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backend_projetdev.Application.UseCases.Equipe.Handlers
{
    public class GetEquipesByDepartementQueryHandler : IRequestHandler<GetEquipesByDepartementQuery, Result<List<EquipeDto>>>
    {
        private readonly IDepartementRepository _departementRepository;
        private readonly IMapper _mapper;

        public GetEquipesByDepartementQueryHandler(IDepartementRepository departementRepository, IMapper mapper)
        {
            _departementRepository = departementRepository;
            _mapper = mapper;
        }

        public async Task<Result<List<EquipeDto>>> Handle(GetEquipesByDepartementQuery request, CancellationToken cancellationToken)
        {
            var departement = await _departementRepository.GetByIdWithEquipesAsync(request.DepartementId);
            if (departement == null)
                return Result<List<EquipeDto>>.Failure("Département introuvable.");

            var equipeDtos = _mapper.Map<List<EquipeDto>>(departement.Equipes);
            return Result<List<EquipeDto>>.SuccessResult(equipeDtos);
        }
    }
}
