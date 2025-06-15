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
    public class GetMesPaiesQueryHandler : IRequestHandler<GetMesPaiesQuery, Result<List<PaieDto>>>
    {
        private readonly IPaieRepository _repository;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUser;

        public GetMesPaiesQueryHandler(IPaieRepository repository, IMapper mapper, ICurrentUserService currentUser)
        {
            _repository = repository;
            _mapper = mapper;
            _currentUser = currentUser;
        }

        public async Task<Result<List<PaieDto>>> Handle(GetMesPaiesQuery request, CancellationToken cancellationToken)
        {
            var userId = await _currentUser.GetUserIdAsync();
            if (userId == null)
                return Result<List<PaieDto>>.Failure("Utilisateur non authentifié.");

            var paies = await _repository.GetByPersonneIdAsync(userId);
            var dtos = _mapper.Map<List<PaieDto>>(paies);
            return Result<List<PaieDto>>.SuccessResult(dtos);
        }
    }
}
