using backend_projetdev.Application.Common;
using MediatR;

namespace backend_projetdev.Application.UseCases.Departement.Commands
{
    public class DeleteDepartementCommand : IRequest<Result>
    {
        public int Id { get; set; }
    }
}
