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
    public class GetAllPaiesQueryHandler : IRequestHandler<GetAllPaiesQuery, Result<List<PaieDto>>>
    {
        private readonly IPaieRepository _repository;
        private readonly IMapper _mapper;

        public GetAllPaiesQueryHandler(IPaieRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result<List<PaieDto>>> Handle(GetAllPaiesQuery request, CancellationToken cancellationToken)
        {
            var paies = await _repository.GetAllAsync();
            var dtos = _mapper.Map<List<PaieDto>>(paies);
            return Result<List<PaieDto>>.SuccessResult(dtos);
        }
    }
}
