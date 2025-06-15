using AutoMapper;
using backend_projetdev.Application.Common;
using backend_projetdev.Application.Interfaces;
using backend_projetdev.Application.UseCases.Poste.Commands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backend_projetdev.Application.UseCases.Poste.Handlers
{
    public class UpdatePosteCommandHandler : IRequestHandler<UpdatePosteCommand, Result>
    {
        private readonly IPosteRepository _posteRepository;
        private readonly IMapper _mapper;

        public UpdatePosteCommandHandler(IPosteRepository posteRepository, IMapper mapper)
        {
            _posteRepository = posteRepository;
            _mapper = mapper;
        }

        public async Task<Result> Handle(UpdatePosteCommand request, CancellationToken cancellationToken)
        {
            var poste = await _posteRepository.GetByIdAsync(request.Id);
            if (poste is null)
                return Result.Failure($"Aucun poste trouvé avec l'ID {request.Id}.");

            _mapper.Map(request.Dto, poste);
            await _posteRepository.UpdateAsync(poste);
            return Result.SuccessResult("Poste mis à jour avec succès.");
        }
    }
}
