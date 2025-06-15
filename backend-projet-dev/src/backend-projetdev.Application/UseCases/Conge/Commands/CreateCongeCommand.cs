using backend_projetdev.Application.Common;
using backend_projetdev.Application.DTOs;
using MediatR;

namespace backend_projetdev.Application.UseCases.Conge.Commands
{
    public class CreateCongeCommand : IRequest<Result<CongeDto>>
    {
        public CreateCongeDto Dto { get; set; }
    }

}
