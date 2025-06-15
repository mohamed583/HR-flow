using AutoMapper;
using backend_projetdev.Application.Common;
using backend_projetdev.Application.DTOs;
using backend_projetdev.Application.Interfaces;
using backend_projetdev.Application.UseCases.Equipe.Commands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backend_projetdev.Application.UseCases.Equipe.Handlers
{
    public class CreateEquipeCommandHandler : IRequestHandler<CreateEquipeCommand, Result<EquipeDto>>
    {
        private readonly IEquipeRepository _equipeRepository;
        private readonly IDepartementRepository _departementRepository;
        private readonly IMapper _mapper;
        public CreateEquipeCommandHandler(IEquipeRepository equipeRepository, IDepartementRepository departementRepository, IMapper mapper)
        {
            _equipeRepository = equipeRepository;
            _departementRepository = departementRepository;
            _mapper = mapper;
        }

        public async Task<Result<EquipeDto>> Handle(CreateEquipeCommand request, CancellationToken cancellationToken)
        {
            var departement = await _departementRepository.GetByIdAsync(request.DepartementId);
            if (departement == null)
                return Result<EquipeDto>.Failure("Département associé introuvable.");

            var equipe = new Domain.Entities.Equipe
            {
                Nom = request.Nom,
                DepartementId = request.DepartementId
            };

            await _equipeRepository.AddAsync(equipe);
            var dto = _mapper.Map<EquipeDto>(equipe);
            return Result<EquipeDto>.SuccessResult(dto, "Équipe créée avec succès.");
        }
    }
}
