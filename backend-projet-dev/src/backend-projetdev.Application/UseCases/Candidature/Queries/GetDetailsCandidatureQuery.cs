using backend_projetdev.Application.DTOs;
using backend_projetdev.Application.Common;
using MediatR;

namespace backend_projetdev.Application.UseCases.Candidature.Queries
{
    public class GetDetailsCandidatureQuery : IRequest<Result<CandidatureDto>>
    {
        public string Id { get; set; }
    }
}
