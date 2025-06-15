using backend_projetdev.Application.Common;
using backend_projetdev.Application.DTOs;
using MediatR;

namespace backend_projetdev.Application.UseCases.Candidature.Commands
{
    public class TransformationEmployeCommand : IRequest<Result>
    {
        public TransformationEmployeDto Data { get; set; }
    }
}
