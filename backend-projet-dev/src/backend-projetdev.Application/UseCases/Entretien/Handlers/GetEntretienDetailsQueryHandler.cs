using AutoMapper;
using backend_projetdev.Application.Common;
using backend_projetdev.Application.DTOs;
using backend_projetdev.Application.Interfaces;
using backend_projetdev.Application.UseCases.Entretien.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backend_projetdev.Application.UseCases.Entretien.Handlers
{
    public class GetEntretienDetailsQueryHandler : IRequestHandler<GetEntretienDetailsQuery, Result<EntretienDto>>
    {
        private readonly IEntretienRepository _repository;
        private readonly IMapper _mapper;

        public GetEntretienDetailsQueryHandler(IEntretienRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result<EntretienDto>> Handle(GetEntretienDetailsQuery request, CancellationToken cancellationToken)
        {
            var entretien = await _repository.GetByIdWithDetailsAsync(request.EntretienId);
            if (entretien == null)
                return Result<EntretienDto>.Failure("Entretien introuvable.");

            var dto = _mapper.Map<EntretienDto>(entretien);
            return Result<EntretienDto>.SuccessResult(dto);
        }
    }

}
