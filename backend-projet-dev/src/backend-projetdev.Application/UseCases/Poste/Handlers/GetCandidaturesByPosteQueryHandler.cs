using AutoMapper;
using backend_projetdev.Application.Common;
using backend_projetdev.Application.DTOs;
using backend_projetdev.Application.Interfaces;
using backend_projetdev.Application.UseCases.Poste.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backend_projetdev.Application.UseCases.Poste.Handlers
{
    public class GetCandidaturesByPosteQueryHandler : IRequestHandler<GetCandidaturesByPosteQuery, Result<List<CandidatureDto>>>
    {
        private readonly IPosteRepository _posteRepository;
        private readonly IMapper _mapper;

        public GetCandidaturesByPosteQueryHandler(IPosteRepository posteRepository, IMapper mapper)
        {
            _posteRepository = posteRepository;
            _mapper = mapper;
        }

        public async Task<Result<List<CandidatureDto>>> Handle(GetCandidaturesByPosteQuery request, CancellationToken cancellationToken)
        {
            var candidatures = await _posteRepository.GetCandidaturesByPosteIdAsync(request.PosteId);
            var dtos = _mapper.Map<List<CandidatureDto>>(candidatures);
            return Result<List<CandidatureDto>>.SuccessResult(dtos);
        }
    }
}
