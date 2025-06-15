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
    public class GetAllPostesQueryHandler : IRequestHandler<GetAllPostesQuery, Result<List<PosteDto>>>
    {
        private readonly IPosteRepository _posteRepository;
        private readonly IMapper _mapper;

        public GetAllPostesQueryHandler(IPosteRepository posteRepository, IMapper mapper)
        {
            _posteRepository = posteRepository;
            _mapper = mapper;
        }

        public async Task<Result<List<PosteDto>>> Handle(GetAllPostesQuery request, CancellationToken cancellationToken)
        {
            var postes = await _posteRepository.GetAllAsync();
            var dtos = _mapper.Map<List<PosteDto>>(postes);
            return Result<List<PosteDto>>.SuccessResult(dtos);
        }
    }

}
