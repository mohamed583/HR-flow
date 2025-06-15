using backend_projetdev.Application.Common;
using MediatR;

namespace backend_projetdev.Application.UseCases.Employe.Commands
{
    public class DeleteEmployeCommand : IRequest<Result>
    {
        public string Id { get; set; }
    }
}
