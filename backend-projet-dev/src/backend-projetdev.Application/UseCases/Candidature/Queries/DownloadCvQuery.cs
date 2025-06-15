using backend_projetdev.Application.Common;
using backend_projetdev.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backend_projetdev.Application.UseCases.Candidature.Queries
{
    public class DownloadCvQuery : IRequest<Result<FileDto>>
    {
        public string CandidatureId { get; set; }

    }
}