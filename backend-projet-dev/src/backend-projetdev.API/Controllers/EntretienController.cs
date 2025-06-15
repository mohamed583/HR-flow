using backend_projetdev.Application.UseCases.Entretien.Commands;
using backend_projetdev.Application.UseCases.Entretien.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend_projetdev.API.Controllers
{
    [ApiController]
    [Route("entretien")]
    [Authorize]
    public class EntretienController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EntretienController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] CreateEntretienCommand command)
        {
            var result = await _mediator.Send(command);
            return result.Success
            ? CreatedAtAction(nameof(GetDetails), new { id = result.Data.Id }, result.Data)
    :       BadRequest(result.Message);
        }

        [HttpGet("Candidature/{candidatureId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetByCandidature(string candidatureId)
        {
            var result = await _mediator.Send(new GetEntretiensByCandidatureQuery { CandidatureId = candidatureId });
            return result.Success ? Ok(result.Data) : NotFound(result.Message);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetDetails(string id)
        {
            var result = await _mediator.Send(new GetEntretienDetailsQuery { EntretienId = id });
            return result.Success ? Ok(result.Data) : NotFound(result.Message);
        }

        [HttpGet("Employe")]
        [Authorize(Roles = "Employe")]
        public async Task<IActionResult> GetForEmploye()
        {
            var result = await _mediator.Send(new GetEntretiensForEmployeQuery());
            return result.Success ? Ok(result.Data) : Unauthorized(result.Message);
        }

        [HttpGet("Employe/NonFinalise")]
        [Authorize(Roles = "Employe")]
        public async Task<IActionResult> GetNonFinalizedForEmploye()
        {
            var result = await _mediator.Send(new GetNonFinalizedEntretiensForEmployeQuery());
            return result.Success ? Ok(result.Data) : Unauthorized(result.Message);
        }

        [HttpPut("Complete/{id}")]
        [Authorize(Roles = "Employe")]
        public async Task<IActionResult> Complete(string id, [FromBody] CompleteEntretienCommand command)
        {
            if (id != command.EntretienId)
                return BadRequest("L'ID ne correspond pas.");

            var result = await _mediator.Send(command);
            return result.Success ? Ok(new { message = result.Message }) : BadRequest(result.Message);
        }
    }
}
