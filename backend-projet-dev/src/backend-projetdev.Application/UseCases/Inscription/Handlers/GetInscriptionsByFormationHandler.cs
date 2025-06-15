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
    public class GetInscriptionsByFormationHandler : IRequestHandler<GetInscriptionsByFormationQuery, Result<List<InscriptionDto>>>
    {
        private readonly IInscriptionRepository _repo;
        private readonly IMapper _mapper;

        public GetInscriptionsByFormationHandler(IInscriptionRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<Result<List<InscriptionDto>>> Handle(GetInscriptionsByFormationQuery request, CancellationToken cancellationToken)
        {
            var inscriptions = await _repo.GetInscriptionsByFormationAsync(request.FormationId);
            var result = _mapper.Map<List<InscriptionDto>>(inscriptions);
            return Result<List<InscriptionDto>>.SuccessResult(result);
        }
    }
}
