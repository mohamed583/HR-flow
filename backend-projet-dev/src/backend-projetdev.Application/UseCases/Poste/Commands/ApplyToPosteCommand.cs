using backend_projetdev.Application.Common;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backend_projetdev.Application.UseCases.Poste.Commands
{
    public class ApplyToPosteCommand : IRequest<Result<string>>
    {
        public int PosteId { get; set; }
        public string CandidatId { get; set; }
        public IFormFile CvFile { get; set; }
    }
}
