using backend_projetdev.Application.Common;
using backend_projetdev.Domain.Enums;
using MediatR;

namespace backend_projetdev.Application.UseCases.Candidature.Commands
{
    public class ModifierStatutCandidatureCommand : IRequest<Result>
    {
        public string Id { get; set; }
        public Status Status { get; set; }
    }
}
