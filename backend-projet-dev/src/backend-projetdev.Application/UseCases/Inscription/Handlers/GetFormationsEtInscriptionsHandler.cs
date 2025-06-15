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
    public class GetFormationsEtInscriptionsHandler : IRequestHandler<GetFormationsEtInscriptionsQuery, Result<List<FormationsEtInscriptionsDto>>>
    {
        private readonly IInscriptionRepository _repo;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;

        public GetFormationsEtInscriptionsHandler(IInscriptionRepository repo, IMapper mapper, ICurrentUserService currentUserService)
        {
            _repo = repo;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        public async Task<Result<List<FormationsEtInscriptionsDto>>> Handle(GetFormationsEtInscriptionsQuery request, CancellationToken cancellationToken)
        {
            var FormateurId = await _currentUserService.GetUserIdAsync();

            var formations = await _repo.GetFormationsWithInscriptionsByFormateurAsync(FormateurId);
            var result = _mapper.Map<List<FormationsEtInscriptionsDto>>(formations);
            return Result<List<FormationsEtInscriptionsDto>>.SuccessResult(result);
        }
    }
}
