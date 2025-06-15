using AutoMapper;
using backend_projetdev.Application.Common;
using backend_projetdev.Application.DTOs;
using backend_projetdev.Application.Interfaces;
using backend_projetdev.Application.UseCases.Evaluation.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backend_projetdev.Application.UseCases.Evaluation.Handlers
{
    public class GetAllEvaluationsHandler : IRequestHandler<GetAllEvaluationsQuery, Result<List<EvaluationDto>>>
    {
        private readonly IEvaluationRepository _repository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;

        public GetAllEvaluationsHandler(IEvaluationRepository repository, ICurrentUserService currentUserService, IMapper mapper)
        {
            _repository = repository;
            _currentUserService = currentUserService;
            _mapper = mapper;
        }

        public async Task<Result<List<EvaluationDto>>> Handle(GetAllEvaluationsQuery request, CancellationToken cancellationToken)
        {
            var userId = await _currentUserService.GetUserIdAsync();

            if (await _currentUserService.IsInRoleAsync("Admin"))
            {
                var all = await _repository.GetAllWithDetailsAsync();
                return Result<List<EvaluationDto>>.SuccessResult(_mapper.Map<List<EvaluationDto>>(all));
            }

            if (await _currentUserService.IsInRoleAsync("Manager"))
            {
                var evaluations = await _repository.GetByManagerEquipeAsync(userId);
                return Result<List<EvaluationDto>>.SuccessResult(_mapper.Map<List<EvaluationDto>>(evaluations));
            }

            return Result<List<EvaluationDto>>.Failure("Non autorisé.");
        }
    }

}
