using AutoMapper;
using backend_projetdev.Application.Common;
using backend_projetdev.Application.DTOs;
using backend_projetdev.Application.Interfaces;
using backend_projetdev.Application.UseCases.Evaluation.Commands;
using backend_projetdev.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backend_projetdev.Application.UseCases.Evaluation.Handlers
{
    public class CreateEvaluationHandler : IRequestHandler<CreateEvaluationCommand, Result<EvaluationDto>>
    {
        private readonly IEvaluationRepository _repository;
        private readonly IEmployeRepository _employeRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;

        public CreateEvaluationHandler(
            IEvaluationRepository repository,
            IEmployeRepository employeRepository,
            ICurrentUserService currentUserService,
            IMapper mapper)
        {
            _repository = repository;
            _employeRepository = employeRepository;
            _currentUserService = currentUserService;
            _mapper = mapper;
        }

        public async Task<Result<EvaluationDto>> Handle(CreateEvaluationCommand request, CancellationToken cancellationToken)
        {
            var userId = await _currentUserService.GetUserIdAsync();
            var employe = await _employeRepository.GetByIdAsync(request.EmployeId);
            var responsable = await _employeRepository.GetByIdAsync(userId);

            if (employe == null || responsable == null)
                return Result<EvaluationDto>.Failure("Employé ou responsable invalide.");

            var evaluation = new Domain.Entities.Evaluation
            {
                EmployeId = employe.Id,
                ResponsableId = responsable.Id,
                Description = request.Description,
                Objectifs = request.Objectifs,
                CommentairesEmploye = "",
                CommentairesResponsable = "",
                Note = 0,
                FinaliseParEmploye = false,
                FinaliseParManager = false,
                EstApprouve = EstApprouve.EnCours,
                DateEvaluation = DateTime.Now
            };

            await _repository.AddAsync(evaluation);

            return Result<EvaluationDto>.SuccessResult(_mapper.Map<EvaluationDto>(evaluation));
        }
    }

}
