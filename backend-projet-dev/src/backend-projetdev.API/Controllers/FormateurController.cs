using backend_projetdev.Application.UseCases.Formateur.Commands;
using backend_projetdev.Application.UseCases.Formateur.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend_projetdev.API.Controllers
{
    [ApiController]
    [Route("formateur")]
    
    public class FormateurController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FormateurController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // 1. Créer un formateur
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateFormateur([FromBody] CreateFormateurCommand command)
        {
            var result = await _mediator.Send(command);
            if (!result.Success)
                return BadRequest(result.Message);

            return CreatedAtAction(nameof(GetFormateur), new { id = result.Data }, result.Data);
        }

        // 2. Lister tous les formateurs
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetFormateurs()
        {
            var result = await _mediator.Send(new GetAllFormateursQuery());
            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result.Data);
        }

        // 3. Afficher un formateur spécifique
        [HttpGet("{id}")]
        [Authorize(Roles = "Employe")]
        public async Task<IActionResult> GetFormateur(string id)
        {
            var result = await _mediator.Send(new GetFormateurByIdQuery { Id = id });
            if (!result.Success)
                return NotFound(result.Message);

            return Ok(result.Data);
        }

        // 4. Modifier un formateur
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditFormateur(string id, [FromBody] EditFormateurCommand command)
        {
            if (id != command.Formateur.Id)
                return BadRequest("L'identifiant ne correspond pas");

            var result = await _mediator.Send(command);
            if (!result.Success)
                return BadRequest(result.Message);

            return NoContent();
        }

        // 5. Supprimer un formateur
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteFormateur(string id)
        {
            var result = await _mediator.Send(new DeleteFormateurCommand { Id = id });
            if (!result.Success)
                return NotFound(result.Message);

            return NoContent();
        }

        
    }
}
