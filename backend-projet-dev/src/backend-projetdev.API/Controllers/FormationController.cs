using backend_projetdev.Application.Common;
using backend_projetdev.Application.UseCases.Formation.Commands;
using backend_projetdev.Application.UseCases.Formation.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace backend_projetdev.API.Controllers
{
    [ApiController]
    [Route("formation")]
    public class FormationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FormationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // 1. GET: /formation/formateur/{formateurId}
        [HttpGet("formateur/{formateurId}")]
        public async Task<IActionResult> GetFormationsByFormateur(string formateurId)
        {
            var result = await _mediator.Send(new GetFormationsByFormateurQuery(formateurId));
            return result.Success ? Ok(result) : NotFound(result);
        }

        // 2. GET: /formation/{id}
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetFormation(int id)
        {
            var result = await _mediator.Send(new GetFormationByIdQuery(id));
            return result.Success ? Ok(result) : NotFound(result);
        }

        // 3. POST: /formation
        [HttpPost]
        public async Task<IActionResult> CreateFormation(CreateFormationCommand command)
        {
            var result = await _mediator.Send(command);
            return result.Success ? CreatedAtAction(nameof(GetFormation), new { id = result.Data.Id }, result) : BadRequest(result);
        }

        // 4. PUT: /formation/{id}
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateFormation(int id, UpdateFormationCommand command)
        {
            if (id != command.Id)
                return BadRequest(Result.Failure("L'ID de la formation ne correspond pas."));

            var result = await _mediator.Send(command);
            return result.Success ? NoContent() : NotFound(result);
        }

        // 5. DELETE: /formation/{id}
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteFormation(int id)
        {
            var result = await _mediator.Send(new DeleteFormationCommand{ Id = id });
            return result.Success ? NoContent() : NotFound(result);
        }
    }
}