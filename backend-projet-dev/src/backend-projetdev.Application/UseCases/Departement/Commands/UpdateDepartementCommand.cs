using backend_projetdev.Application.Common;
using backend_projetdev.Application.DTOs;
using MediatR;

namespace backend_projetdev.Application.UseCases.Departement.Commands
{
    public class UpdateDepartementCommand : IRequest<Result<DepartementDetailsDto>>
    {
        public int Id { get; set; }
        public DepartementDto Dto { get; set; }
    }

}