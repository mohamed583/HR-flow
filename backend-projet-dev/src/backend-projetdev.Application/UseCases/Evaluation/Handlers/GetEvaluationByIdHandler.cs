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
    public class GetEvaluationByIdHandler : IRequestHandler<GetEvaluationByIdQuery, Result<EvaluationDto>>
    {
        private readonly IEvaluationRepository _repository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;

        public GetEvaluationByIdHandler(IEvaluationRepository repository, ICurrentUserService currentUserService, IMapper mapper)
        {
            _repository = repository;
            _currentUserService = currentUserService;
            _mapper = mapper;
        }

        public async Task<Result<EvaluationDto>> Handle(GetEvaluationByIdQuery request, CancellationToken cancellationToken)
        {
            var evaluation = await _repository.GetByIdWithDetailsAsync(request.Id);
            if (evaluation == null)
                return Result<EvaluationDto>.Failure("Évaluation non trouvée.");

            var userId = await _currentUserService.GetUserIdAsync();

            if (await _currentUserService.IsInRoleAsync("Admin") ||
                (await _currentUserService.IsInRoleAsync("Manager") && evaluation.ResponsableId == userId) ||
                (await _currentUserService.IsInRoleAsync("Employe") && evaluation.EmployeId == userId))
            {
                return Result<EvaluationDto>.SuccessResult(_mapper.Map<EvaluationDto>(evaluation));
            }

            return Result<EvaluationDto>.Failure("Non autorisé.");
        }
    }

}
