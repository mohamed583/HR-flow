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
    public class GetEntretiensForEmployeQueryHandler : IRequestHandler<GetEntretiensForEmployeQuery, Result<List<EntretienDto>>>
    {
        private readonly IEntretienRepository _repository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;

        public GetEntretiensForEmployeQueryHandler(IEntretienRepository repository, ICurrentUserService currentUserService, IMapper mapper)
        {
            _repository = repository;
            _currentUserService = currentUserService;
            _mapper = mapper;
        }

        public async Task<Result<List<EntretienDto>>> Handle(GetEntretiensForEmployeQuery request, CancellationToken cancellationToken)
        {
            var employeId = await _currentUserService.GetUserIdAsync();
            var entretiens = await _repository.GetByEmployeIdAsync(employeId);

            var dtoList = _mapper.Map<List<EntretienDto>>(entretiens);
            return Result<List<EntretienDto>>.SuccessResult(dtoList);
        }
    }

}
