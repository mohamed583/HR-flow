using AutoMapper;
using backend_projetdev.Application.Common;
using backend_projetdev.Application.DTOs;
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
    public class CreatePosteCommandHandler : IRequestHandler<CreatePosteCommand, Result<PosteDto>>
    {
        private readonly IPosteRepository _posteRepository;
        private readonly IMapper _mapper;

        public CreatePosteCommandHandler(IPosteRepository posteRepository, IMapper mapper)
        {
            _posteRepository = posteRepository;
            _mapper = mapper;
        }

        public async Task<Result<PosteDto>> Handle(CreatePosteCommand request, CancellationToken cancellationToken)
        {
            var poste = _mapper.Map<Domain.Entities.Poste>(request.Information);
            await _posteRepository.AddAsync(poste);
            var dto = _mapper.Map<PosteDto>(poste);
            return Result<PosteDto>.SuccessResult(dto, "Poste créé avec succès.");
        }
    }
}
