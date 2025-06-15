using AutoMapper;
using backend_projetdev.Application.Common;
using backend_projetdev.Application.DTOs;
using backend_projetdev.Application.Interfaces;
using backend_projetdev.Application.UseCases.Paie.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backend_projetdev.Application.UseCases.Paie.Handlers
{
    public class GetPaieByIdQueryHandler : IRequestHandler<GetPaieByIdQuery, Result<PaieDto>>
    {
        private readonly IPaieRepository _repository;
        private readonly IMapper _mapper;

        public GetPaieByIdQueryHandler(IPaieRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result<PaieDto>> Handle(GetPaieByIdQuery request, CancellationToken cancellationToken)
        {
            var paie = await _repository.GetByIdAsync(request.Id);
            if (paie == null)
                return Result<PaieDto>.Failure("Paie non trouvée.");

            var dto = _mapper.Map<PaieDto>(paie);
            return Result<PaieDto>.SuccessResult(dto);
        }
    }
}
