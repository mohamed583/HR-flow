using backend_projetdev.Application.Common;
using backend_projetdev.Application.DTOs;
using MediatR;

namespace backend_projetdev.Application.UseCases.Employe.Queries
{
    public class GetEmployesByEquipeQuery : IRequest<Result<List<EmployeDto>>>
    {
        public int EquipeId { get; set; }
    }
}
