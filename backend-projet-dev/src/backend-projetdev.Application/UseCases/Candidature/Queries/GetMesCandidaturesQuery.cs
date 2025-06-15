using backend_projetdev.Application.DTOs;
using backend_projetdev.Application.Common;
using MediatR;

namespace backend_projetdev.Application.UseCases.Candidature.Queries
{
    public class GetMesCandidaturesQuery : IRequest<Result<List<CandidatureDto>>>
    {
        public string CandidatId { get; set; }
    }
}
