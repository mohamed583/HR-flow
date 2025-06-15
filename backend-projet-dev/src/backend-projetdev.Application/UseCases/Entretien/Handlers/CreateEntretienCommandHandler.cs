using AutoMapper;
using backend_projetdev.Application.Common;
using backend_projetdev.Application.DTOs;
using backend_projetdev.Application.Interfaces;
using backend_projetdev.Application.UseCases.Entretien.Commands;
using backend_projetdev.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace backend_projetdev.Application.UseCases.Entretien.Handlers
{
    public class CreateEntretienCommandHandler : IRequestHandler<CreateEntretienCommand, Result<EntretienDto>>
    {
        private readonly IEntretienRepository _repository;
        private readonly ICandidatureRepository _candidatureRepository;
        private readonly IMapper _mapper;
        private readonly IEmployeRepository _employeRepository;

        public CreateEntretienCommandHandler(IEntretienRepository repository, ICandidatureRepository candidatureRepository, IEmployeRepository employeRepository)
        {
            _repository = repository;
            _candidatureRepository = candidatureRepository;
            _employeRepository = employeRepository;
        }

        public async Task<Result<EntretienDto>> Handle(CreateEntretienCommand request, CancellationToken cancellationToken)
        {
            var candidature = await _candidatureRepository.GetByIdAsync(request.Model.CandidatureId);
            if (candidature == null)
                return Result<EntretienDto>.Failure("Candidature introuvable.");

            var entretien = new Domain.Entities.Entretien
            {
                Id = Guid.NewGuid().ToString(),
                CandidatureId = request.Model.CandidatureId,
                EmployeId = request.Model.EmployeId,
                DateEntretien = request.Model.DateEntretien,
                Commentaire = "",
                Status = StatusEntretien.NonCommence
            };

            await _repository.AddAsync(entretien);
            entretien.Candidature = candidature;
            entretien.Employe = await _employeRepository.GetByIdAsync(entretien.EmployeId);
            var dto = new EntretienDto
            {
                Id = entretien.Id,
                CandidatureId = entretien.CandidatureId,
                EmployeId = entretien.EmployeId,
                DateEntretien = entretien.DateEntretien,
                Status = entretien.Status.ToString()
            };
            return Result<EntretienDto>.SuccessResult(dto);
        }

    }

}
