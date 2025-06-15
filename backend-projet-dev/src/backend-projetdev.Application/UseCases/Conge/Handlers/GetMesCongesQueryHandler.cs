using AutoMapper;
using backend_projetdev.Application.Common;
using backend_projetdev.Application.DTOs;
using backend_projetdev.Application.Interfaces;
using backend_projetdev.Application.UseCases.Conge.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backend_projetdev.Application.UseCases.Conge.Handlers
{
    public class GetMesCongesQueryHandler : IRequestHandler<GetMesCongesQuery, Result<List<CongeDto>>>
    {
        private readonly ICongeRepository _repository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IEmployeService _employeService;
        private readonly IMapper _mapper;

        public GetMesCongesQueryHandler(
            ICongeRepository repository,
            ICurrentUserService currentUserService,
            IEmployeService employeService,
            IMapper mapper)
        {
            _repository = repository;
            _currentUserService = currentUserService;
            _employeService = employeService;
            _mapper = mapper;
        }

        public async Task<Result<List<CongeDto>>> Handle(GetMesCongesQuery request, CancellationToken cancellationToken)
        {
            var userId = await _currentUserService.GetUserIdAsync();
            var employe = await _employeService.GetByIdAsync(userId);
            if (employe == null)
                return Result<List<CongeDto>>.Failure("Employé non trouvé.");

            var conges = await _repository.GetByEmployeIdAsync(employe.Id);
            var dto = _mapper.Map<List<CongeDto>>(conges);
            return Result<List<CongeDto>>.SuccessResult(dto);
        }
    }
}
