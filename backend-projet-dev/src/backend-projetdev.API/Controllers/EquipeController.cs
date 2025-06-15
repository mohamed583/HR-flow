using backend_projetdev.Application.UseCases.Equipe.Commands;
using backend_projetdev.Application.UseCases.Equipe.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend_projetdev.API.Controllers
{
    [ApiController]
    [Route("equipe")]
    [Authorize(Roles = "Admin")]
    public class EquipeController : ControllerBase
    {
        private readonly IMediator _sender;

        public EquipeController(IMediator sender)
        {
            _sender = sender;
        }

        [HttpGet("Departement/{departementId}")]
        public async Task<IActionResult> GetByDepartement(int departementId)
        {
            var result = await _sender.Send(new GetEquipesByDepartementQuery { DepartementId = departementId });

            return result.Success
                ? Ok(result.Data)
                : NotFound(new { message = result.Message });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _sender.Send(new GetEquipeByIdQuery { Id = id });

            return result.Success
                ? Ok(result.Data)
                : NotFound(new { message = result.Message });
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateEquipeCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _sender.Send(command);

            return result.Success
                ? CreatedAtAction(nameof(GetById), new { id = result.Data.Id }, result.Data)
                : BadRequest(new { message = result.Message });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateEquipeCommand command)
        {
            if (id != command.Id)
                return BadRequest(new { message = "ID incohérent." });

            var result = await _sender.Send(command);

            return result.Success
                ? Ok(new { message = "Équipe mise à jour avec succès." })
                : NotFound(new { message = result.Message });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _sender.Send(new DeleteEquipeCommand { Id = id });

            return result.Success
                ? NoContent()
                : NotFound(new { message = result.Message });
        }
    }
}
