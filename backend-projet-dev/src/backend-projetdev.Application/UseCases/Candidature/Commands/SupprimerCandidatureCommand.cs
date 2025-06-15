using backend_projetdev.Application.Common;
using MediatR;
namespace backend_projetdev.Application.UseCases.Candidature.Commands
{
    public class SupprimerCandidatureCommand : IRequest<Result>
    {
        public string Id { get; set; }
        public string UserId { get; set; }
    }

}
