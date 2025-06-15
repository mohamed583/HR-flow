using AutoMapper;
using backend_projetdev.Application.Common;
using backend_projetdev.Application.DTOs;
using backend_projetdev.Application.Interfaces;
using backend_projetdev.Application.UseCases.Formation.Commands;
using backend_projetdev.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backend_projetdev.Application.UseCases.Formation.Handlers
{
    public class CreateFormationCommandHandler : IRequestHandler<CreateFormationCommand, Result<FormationDto>>
    {
        private readonly IFormationRepository _formationRepository;
        private readonly IFormateurRepository _formateurRepository;
        private readonly IMapper _mapper;

        public CreateFormationCommandHandler(
            IFormationRepository formationRepository,
            IFormateurRepository formateurRepository,
            IMapper mapper)
        {
            _formationRepository = formationRepository;
            _formateurRepository = formateurRepository;
            _mapper = mapper;
        }

        public async Task<Result<FormationDto>> Handle(CreateFormationCommand request, CancellationToken cancellationToken)
        {
            var formateur = await _formateurRepository.GetByIdAsync(request.FormateurId);
            if (formateur == null)
                return Result<FormationDto>.Failure("Formateur introuvable.");

            var formation = new Domain.Entities.Formation
            {
                Titre = request.Titre,
                Description = request.Description,
                DateDebut = request.DateDebut,
                DateFin = request.DateFin,
                FormateurId = request.FormateurId,
                Cout = request.Cout
            };
            
            await _formationRepository.AddAsync(formation);

            var dto = _mapper.Map<FormationDto>(formation);
            return Result<FormationDto>.SuccessResult(dto);
        }
    }

}
