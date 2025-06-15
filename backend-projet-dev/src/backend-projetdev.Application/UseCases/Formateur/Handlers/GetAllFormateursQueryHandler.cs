using AutoMapper;
using backend_projetdev.Application.Common;
using backend_projetdev.Application.DTOs;
using backend_projetdev.Application.Interfaces;
using backend_projetdev.Application.UseCases.Formateur.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backend_projetdev.Application.UseCases.Formateur.Handlers
{
    public class GetAllFormateursQueryHandler : IRequestHandler<GetAllFormateursQuery, Result<List<FormateurDto>>>
    {
        private readonly IFormateurRepository _repository;
        private readonly IMapper _mapper;

        public GetAllFormateursQueryHandler(IFormateurRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result<List<FormateurDto>>> Handle(GetAllFormateursQuery request, CancellationToken cancellationToken)
        {
            var formateurs = await _repository.GetAllAsync();
            var dtos = _mapper.Map<List<FormateurDto>>(formateurs);
            return Result<List<FormateurDto>>.SuccessResult(dtos);
        }
    }
}
