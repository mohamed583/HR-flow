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
    public class GetEquipeByIdQueryHandler : IRequestHandler<GetEquipeByIdQuery, Result<EquipeDto>>
    {
        private readonly IEquipeRepository _equipeRepository;
        private readonly IMapper _mapper;

        public GetEquipeByIdQueryHandler(IEquipeRepository equipeRepository, IMapper mapper)
        {
            _equipeRepository = equipeRepository;
            _mapper = mapper;
        }

        public async Task<Result<EquipeDto>> Handle(GetEquipeByIdQuery request, CancellationToken cancellationToken)
        {
            var equipe = await _equipeRepository.GetWithDetailsByIdAsync(request.Id);
            if (equipe == null)
                return Result<EquipeDto>.Failure("Équipe introuvable.");

            var dto = _mapper.Map<EquipeDto>(equipe);
            return Result<EquipeDto>.SuccessResult(dto);
        }
    }
}