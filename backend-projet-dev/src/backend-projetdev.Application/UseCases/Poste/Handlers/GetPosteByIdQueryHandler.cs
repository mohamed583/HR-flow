using AutoMapper;
using backend_projetdev.Application.Common;
using backend_projetdev.Application.DTOs;
using backend_projetdev.Application.Interfaces;
using backend_projetdev.Application.UseCases.Poste.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backend_projetdev.Application.UseCases.Poste.Handlers
{
    public class GetPosteByIdQueryHandler : IRequestHandler<GetPosteByIdQuery, Result<PosteDto>>
    {
        private readonly IPosteRepository _posteRepository;
        private readonly IMapper _mapper;

        public GetPosteByIdQueryHandler(IPosteRepository posteRepository, IMapper mapper)
        {
            _posteRepository = posteRepository;
            _mapper = mapper;
        }

        public async Task<Result<PosteDto>> Handle(GetPosteByIdQuery request, CancellationToken cancellationToken)
        {
            var poste = await _posteRepository.GetByIdAsync(request.Id);
            if (poste is null)
                return Result<PosteDto>.Failure($"Aucun poste trouvé avec l'ID {request.Id}.");

            var dto = _mapper.Map<PosteDto>(poste);
            return Result<PosteDto>.SuccessResult(dto);
        }
    }
}
