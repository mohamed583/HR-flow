using backend_projetdev.Application.DTOs;
using backend_projetdev.Application.Common;
using MediatR;

namespace backend_projetdev.Application.UseCases.Candidature.Queries
{
    public class GetCandidaturesParPosteQuery : IRequest<Result<List<CandidatureDto>>>
    {
        public int PosteId { get; set; }
    }
}
