using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using backend_projetdev.Application.Common;
using backend_projetdev.Application.DTOs;
using backend_projetdev.Application.Interfaces;
using backend_projetdev.Application.UseCases.Candidature.Queries;
using MediatR;

namespace backend_projetdev.Application.UseCases.Candidature.Handlers
{
    public class GetCvMatchByPosteIdQueryHandler : IRequestHandler<GetCvMatchByPosteIdQuery, Result<List<CvMatchResultDto>>>
    {
        private readonly ICvMatchingService _cvMatchingService;

        public GetCvMatchByPosteIdQueryHandler(ICvMatchingService cvMatchingService)
        {
            _cvMatchingService = cvMatchingService;
        }

        public async Task<Result<List<CvMatchResultDto>>> Handle(GetCvMatchByPosteIdQuery request, CancellationToken cancellationToken)
        {
            var dto = await _cvMatchingService.GetMatchingsAsync(request.PosteId);
            return Result<List<CvMatchResultDto>>.SuccessResult(dto);
        }
    }
}
