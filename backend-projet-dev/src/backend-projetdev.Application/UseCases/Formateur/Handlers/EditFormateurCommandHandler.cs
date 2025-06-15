using AutoMapper;
using backend_projetdev.Application.Common;
using backend_projetdev.Application.Interfaces;
using backend_projetdev.Application.UseCases.Formateur.Commands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backend_projetdev.Application.UseCases.Formateur.Handlers
{
    public class EditFormateurCommandHandler : IRequestHandler<EditFormateurCommand, Result>
    {
        private readonly IFormateurRepository _repository;
        private readonly IMapper _mapper;

        public EditFormateurCommandHandler(IFormateurRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result> Handle(EditFormateurCommand request, CancellationToken cancellationToken)
        {
            var formateur = await _repository.GetByIdAsync(request.Formateur.Id);
            if (formateur == null)
                return Result.Failure("Formateur non trouvé.");

            _mapper.Map(request.Formateur, formateur);

            var success = await _repository.UpdateAsync(formateur);
            if (!success)
                return Result.Failure("Échec de la mise à jour du formateur.");

            return Result.SuccessResult("Formateur mis à jour avec succès.");
        }
    }
}
