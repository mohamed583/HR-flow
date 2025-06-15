using backend_projetdev.Application.Common;
using backend_projetdev.Application.DTOs;
using MediatR;
namespace backend_projetdev.Application.UseCases.Employe.Commands
{
    public class EditEmployeCommand : IRequest<Result>
    {
        public EditEmployeDto Data { get; set; }
    }

}
