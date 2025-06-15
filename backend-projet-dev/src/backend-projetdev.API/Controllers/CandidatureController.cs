using backend_projetdev.Application.DTOs;
using backend_projetdev.Application.UseCases.Candidature.Commands;
using backend_projetdev.Application.UseCases.Candidature.Queries;
using backend_projetdev.Domain.Entities;
using backend_projetdev.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend_projetdev.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("candidature")]
    public class CandidatureController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CandidatureController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("candidatures/{id}")]
        public async Task<IActionResult> GetCandidaturesByPosteId(int id)
        {
            var result = await _mediator.Send(new GetCandidaturesParPosteQuery { PosteId = id });
            return result.Success ? Ok(result.Data) : NotFound(result.Message);
        }

        [Authorize(Roles = "Candidat")]
        [HttpGet("mes-candidatures")]
        public async Task<IActionResult> GetMesCandidatures()
        {
            var result = await _mediator.Send(new GetMesCandidaturesQuery());
            return result.Success ? Ok(result.Data != null && result.Data.Any() ? result.Data : result.Message) : NotFound(result.Message);
        }

        [HttpGet("details/{id}")]
        public async Task<IActionResult> GetDetails(string id)
        {
            var result = await _mediator.Send(new GetDetailsCandidatureQuery { Id = id });
            return result.Success ? Ok(result.Data) : NotFound(result.Message);

        }

        [Authorize(Roles = "Admin")]
        [HttpPost("modifier-statut/{id}")]
        public async Task<IActionResult> ModifierStatut(string id, [FromBody] Status status)
        {
            var result = await _mediator.Send(new ModifierStatutCandidatureCommand { Id = id, Status = status });
            return result.Success ? Ok(result.Message) : NotFound(result.Message);
        }

        [Authorize(Roles = "Candidat")]
        [HttpDelete("supprimer-candidature/{id}")]
        public async Task<IActionResult> SupprimerCandidature(string id)
        {
            var result = await _mediator.Send(new SupprimerCandidatureCommand { Id = id });
            return result.Success ? Ok(result.Message) : NotFound(result.Message);
        }

        [HttpGet("download-cv/{id}")]
        public async Task<IActionResult> DownloadCv(string id)
        {
            var result = await _mediator.Send(new DownloadCvQuery { CandidatureId = id });

            if (!result.Success)
                return NotFound(result.Message);

            var fileResult = result.Data;
            return File(fileResult.FileBytes, fileResult.ContentType, fileResult.FileName);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("transformation-employe")]
        public async Task<IActionResult> TransformationEmploye([FromBody] TransformationEmployeDto model)
        {
            
            var result = await _mediator.Send(new TransformationEmployeCommand { Data = model });
            return result.Success ? Ok(result.Message) : BadRequest(result.Message);
        }
    }
}
