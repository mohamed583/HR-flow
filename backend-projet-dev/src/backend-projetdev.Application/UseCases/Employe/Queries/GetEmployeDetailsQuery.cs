using backend_projetdev.Application.Common;
using backend_projetdev.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backend_projetdev.Application.UseCases.Employe.Queries
{
    public class GetEmployeDetailsQuery : IRequest<Result<EmployeDto>>
    {
        public string Id { get; set; }
    }
}
