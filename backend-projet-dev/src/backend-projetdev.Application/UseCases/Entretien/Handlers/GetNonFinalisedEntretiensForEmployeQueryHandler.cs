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
    public class GetNonFinalisedEntretiensForEmployeQueryHandler : IRequestHandler<GetNonFinalizedEntretiensForEmployeQuery, Result<List<EntretienDto>>>
    {
        private readonly IEntretienRepository _repository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;

        public GetNonFinalisedEntretiensForEmployeQueryHandler(IEntretienRepository repository, ICurrentUserService currentUserService, IMapper mapper)
        {
            _repository = repository;
            _currentUserService = currentUserService;
            _mapper = mapper;
        }

        public async Task<Result<List<EntretienDto>>> Handle(GetNonFinalizedEntretiensForEmployeQuery request, CancellationToken cancellationToken)
        {
            var employeId = await _currentUserService.GetUserIdAsync();
            var entretiens = await _repository.GetNonFinalisedByEmployeIdAsync(employeId);
            var dtoList = _mapper.Map<List<EntretienDto>>(entretiens);
            return Result<List<EntretienDto>>.SuccessResult(dtoList);
        }
    }

}
