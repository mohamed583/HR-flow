using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using backend_projetdev.Application.Common;
using backend_projetdev.Application.DTOs;
using backend_projetdev.Application.Interfaces;
using backend_projetdev.Application.UseCases.Employe.Queries;
using MediatR;

namespace backend_projetdev.Application.UseCases.Employe.Handlers
{
    public class GetEmployesQueryHandler : IRequestHandler<GetEmployesQuery, Result<List<EmployeDto>>>
    {
        private readonly IEmployeRepository _employeRepository;
        private readonly IMapper _mapper;

        public GetEmployesQueryHandler(
            IEmployeRepository employeRepository,
            IMapper mapper)
        {
            _employeRepository = employeRepository;
            _mapper = mapper;
        }

        public async Task<Result<List<EmployeDto>>> Handle(GetEmployesQuery request, CancellationToken cancellationToken)
        {
            var employes = await _employeRepository.GetAllAsync();
            var dtoList = _mapper.Map<List<EmployeDto>>(employes);

            return Result<List<EmployeDto>>.SuccessResult(dtoList);
        }
    }
}
