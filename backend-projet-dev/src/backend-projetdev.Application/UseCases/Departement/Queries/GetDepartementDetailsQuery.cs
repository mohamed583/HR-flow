using backend_projetdev.Application.Common;
using backend_projetdev.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backend_projetdev.Application.UseCases.Departement.Queries
{
    public class GetDepartementDetailsQuery : IRequest<Result<DepartementDetailsDto>>
    {
        public int Id { get; set; }
    }
}
