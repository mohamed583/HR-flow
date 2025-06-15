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
    public class GetFormateurByIdQueryHandler : IRequestHandler<GetFormateurByIdQuery, Result<FormateurDto>>
    {
        private readonly IFormateurRepository _repository;
        private readonly IMapper _mapper;

        public GetFormateurByIdQueryHandler(IFormateurRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result<FormateurDto>> Handle(GetFormateurByIdQuery request, CancellationToken cancellationToken)
        {
            var formateur = await _repository.GetByIdAsync(request.Id);
            if (formateur == null)
                return Result<FormateurDto>.Failure("Formateur introuvable.");

            return Result<FormateurDto>.SuccessResult(_mapper.Map<FormateurDto>(formateur));
        }
    }
}
