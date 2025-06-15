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
    public class GetMesInscriptionsHandler : IRequestHandler<GetMesInscriptionsQuery, Result<List<InscriptionDto>>>
    {
        private readonly IInscriptionRepository _repo;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;

        public GetMesInscriptionsHandler(IInscriptionRepository repo, IMapper mapper, ICurrentUserService currentUserService)
        {
            _repo = repo;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        public async Task<Result<List<InscriptionDto>>> Handle(GetMesInscriptionsQuery request, CancellationToken cancellationToken)
        {
            var EmployeId = await _currentUserService.GetUserIdAsync();
            var inscriptions = await _repo.GetMesInscriptionsAsync(EmployeId);
            var result = _mapper.Map<List<InscriptionDto>>(inscriptions);
            return Result<List<InscriptionDto>>.SuccessResult(result);
        }
    }
}
