using AutoMapper;
using backend_projetdev.Application.Common;
using backend_projetdev.Application.DTOs;
using backend_projetdev.Application.Interfaces;
using backend_projetdev.Application.UseCases.Paie.Commands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backend_projetdev.Application.UseCases.Paie.Handlers
{
    public class CreatePaieCommandHandler : IRequestHandler<CreatePaieCommand, Result<PaieDto>>
    {
        private readonly IPaieRepository _repository;
        private readonly IPersonneService _personneService;
        private readonly IMapper _mapper;

        public CreatePaieCommandHandler(IPaieRepository repository, IPersonneService personneService, IMapper mapper)
        {
            _repository = repository;
            _personneService = personneService;
            _mapper = mapper;
        }

        public async Task<Result<PaieDto>> Handle(CreatePaieCommand request, CancellationToken cancellationToken)
        {
            var personne = await _personneService.GetByIdAsync(request.PersonneId);
            if (personne == null)
                return Result<PaieDto>.Failure("Personne non trouvée.");

            var paie = new Domain.Entities.Paie
            {
                PersonneId = request.PersonneId,
                DatePaie = request.DatePaie,
                Montant = request.Montant,
                Description = request.Description,
                Avantages = request.Avantages,
                Retenues = request.Retenues
            };

            await _repository.AddAsync(paie);
            paie.Personne = personne;
            var dto = _mapper.Map<PaieDto>(paie);
            return Result<PaieDto>.SuccessResult(dto, "Paie créée avec succès.");
        }
    }
}
