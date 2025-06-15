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
    public class GetMyEvaluationsHandler : IRequestHandler<GetMyEvaluationsQuery, Result<List<EvaluationDto>>>
    {
        private readonly IEvaluationRepository _repository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;

        public GetMyEvaluationsHandler(IEvaluationRepository repository, ICurrentUserService currentUserService, IMapper mapper)
        {
            _repository = repository;
            _currentUserService = currentUserService;
            _mapper = mapper;
        }

        public async Task<Result<List<EvaluationDto>>> Handle(GetMyEvaluationsQuery request, CancellationToken cancellationToken)
        {
            var userId = await _currentUserService.GetUserIdAsync();
            var evaluations = await _repository.GetByEmployeIdAsync(userId);
            return Result<List<EvaluationDto>>.SuccessResult(_mapper.Map<List<EvaluationDto>>(evaluations));
        }
    }

}
