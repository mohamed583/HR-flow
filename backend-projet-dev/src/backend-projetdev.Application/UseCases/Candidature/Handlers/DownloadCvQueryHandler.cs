using backend_projetdev.Application.Common;
using backend_projetdev.Application.DTOs;
using backend_projetdev.Application.Interfaces;
using backend_projetdev.Application.UseCases.Candidature.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backend_projetdev.Application.UseCases.Candidature.Handlers
{
    public class DownloadCvQueryHandler : IRequestHandler<DownloadCvQuery, Result<FileDto>>
    {
        private readonly ICandidatureRepository _candidatureRepository;

        public DownloadCvQueryHandler(ICandidatureRepository candidatureRepository)
        {
            _candidatureRepository = candidatureRepository;
        }

        public async Task<Result<FileDto>> Handle(DownloadCvQuery request, CancellationToken cancellationToken)
        {
            var candidature = await _candidatureRepository.GetByIdAsync(request.CandidatureId);

            if (candidature == null || string.IsNullOrWhiteSpace(candidature.CVPath))
                return Result<FileDto>.Failure("Candidature ou CV introuvable.");

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", candidature.CVPath.TrimStart('/'));
            if (!System.IO.File.Exists(filePath))
                return Result<FileDto>.Failure("Le fichier n'existe pas.");

            var fileBytes = await File.ReadAllBytesAsync(filePath);
            var fileName = Path.GetFileName(filePath);

            var fileDto = new FileDto
            {
                FileBytes = fileBytes,
                FileName = fileName,
                ContentType = "application/octet-stream"
            };

            return Result<FileDto>.SuccessResult(fileDto);
        }
    }
}