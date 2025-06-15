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
    public class GetEmployeDetailsQueryHandler : IRequestHandler<GetEmployeDetailsQuery, Result<EmployeDto>>
    {
        private readonly IEmployeService _employeService;
        private readonly IMapper _mapper;

        public GetEmployeDetailsQueryHandler(IEmployeService employeService, IMapper mapper)
        {
            _employeService = employeService;
            _mapper = mapper;
        }

        public async Task<Result<EmployeDto>> Handle(GetEmployeDetailsQuery request, CancellationToken cancellationToken)
        {
            var employe = await _employeService.GetByIdAsync(request.Id);
            if (employe == null)
                return Result<EmployeDto>.Failure("Employé non trouvé.");

            var dto = _mapper.Map<EmployeDto>(employe);
            return Result<EmployeDto>.SuccessResult(dto);
        }
    }
}