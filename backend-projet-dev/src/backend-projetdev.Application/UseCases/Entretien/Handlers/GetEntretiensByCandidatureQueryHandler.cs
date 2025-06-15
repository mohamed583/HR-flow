using AutoMapper;
using backend_projetdev.Application.Common;
using backend_projetdev.Application.DTOs;
using backend_projetdev.Application.Interfaces;
using backend_projetdev.Application.UseCases.Entretien.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backend_projetdev.Application.UseCases.Entretien.Handlers
{
    public class GetEntretiensByCandidatureQueryHandler : IRequestHandler<GetEntretiensByCandidatureQuery, Result<List<EntretienDto>>>
    {
        private readonly IEntretienRepository _repository;
        private readonly IMapper _mapper;

        public GetEntretiensByCandidatureQueryHandler(IEntretienRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result<List<EntretienDto>>> Handle(GetEntretiensByCandidatureQuery request, CancellationToken cancellationToken)
        {
            var entretiens = await _repository.GetByCandidatureIdAsync(request.CandidatureId);
            var dtoList = _mapper.Map<List<EntretienDto>>(entretiens);
            return Result<List<EntretienDto>>.SuccessResult(dtoList);
        }
    }

}
