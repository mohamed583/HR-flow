using backend_projetdev.Application.Common;
using backend_projetdev.Application.DTOs;
using backend_projetdev.Application.UseCases.Inscription.Commands;
using backend_projetdev.Application.UseCases.Inscription.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend_projetdev.API.Controllers
{
    [ApiController]
    [Route("inscription")]
    public class InscriptionController : ControllerBase
    {
        private readonly IMediator _mediator;

        public InscriptionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("formation/{formationId:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Result<IEnumerable<InscriptionDto>>>> GetInscriptionsByFormation(int formationId)
        {
            var result = await _mediator.Send(new GetInscriptionsByFormationQuery { FormationId = formationId });
            return Ok(result);
        }

        [HttpPost("{id:int}/approve")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ApproveInscription(int id)
        {
            var result = await _mediator.Send(new ApproveInscriptionCommand { InscriptionId = id });
            return result.Success ? NoContent() : BadRequest(result);
        }

        [HttpPost("{id:int}/reject")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RejectInscription(int id)
        {
            var result = await _mediator.Send(new RejectInscriptionCommand { InscriptionId = id });
            return result.Success ? NoContent() : BadRequest(result);
        }

        [HttpGet("formations-disponibles")]
        [Authorize(Roles = "Employe")]
        public async Task<ActionResult<Result<IEnumerable<FormationDto>>>> GetFormationsDisponibles()
        {
            var result = await _mediator.Send(new GetFormationsDisponiblesQuery());
            return Ok(result);
        }

        [HttpPost("postuler/{formationId:int}")]
        [Authorize(Roles = "Employe")]
        public async Task<IActionResult> Postuler(int formationId)
        {
            var result = await _mediator.Send(new PostulerFormationCommand { FormationId = formationId });
            return result.Success ? Created("", result) : BadRequest(result);
        }

        [HttpGet("mes-inscriptions")]
        [Authorize(Roles = "Employe")]
        public async Task<ActionResult<Result<IEnumerable<InscriptionDto>>>> GetMesInscriptions()
        {
            var result = await _mediator.Send(new GetMesInscriptionsQuery());
            return Ok(result);
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Employe")]
        public async Task<IActionResult> SupprimerInscription(int id)
        {
            var result = await _mediator.Send(new SupprimerInscriptionCommand { InscriptionId = id });
            return result.Success ? NoContent() : BadRequest(result);
        }

        [HttpGet("mes-formations")]
        [Authorize(Roles = "Formateur")]
        public async Task<ActionResult<Result<IEnumerable<FormationDto>>>> GetFormationsEtInscriptions()
        {
            var result = await _mediator.Send(new GetFormationsEtInscriptionsQuery());
            return Ok(result);
        }
    }
}
