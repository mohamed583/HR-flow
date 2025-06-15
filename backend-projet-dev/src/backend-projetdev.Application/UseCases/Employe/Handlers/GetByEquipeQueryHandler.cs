using AutoMapper;
using backend_projetdev.Application.Common;
using backend_projetdev.Application.DTOs;
using backend_projetdev.Application.Interfaces;
using backend_projetdev.Application.UseCases.Employe.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backend_projetdev.Application.UseCases.Employe.Handlers
{
    public class GetByEquipeQueryHandler : IRequestHandler<GetEmployesByEquipeQuery, Result<List<EmployeDto>>>
    {
        private readonly IEquipeRepository _equipeRepository;
        private readonly IMapper _mapper;

        public GetByEquipeQueryHandler(IEquipeRepository equipeRepository, IMapper mapper)
        {
            _equipeRepository = equipeRepository;
            _mapper = mapper;
        }

        public async Task<Result<List<EmployeDto>>> Handle(GetEmployesByEquipeQuery request, CancellationToken cancellationToken)
        {
            var employes = await _equipeRepository.GetEmployesByEquipeAsync(request.EquipeId);
            var dtoList = _mapper.Map<List<EmployeDto>>(employes);
            return Result<List<EmployeDto>>.SuccessResult(dtoList);
        }
    }
}