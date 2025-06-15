using backend_projetdev.Application.Common;
using backend_projetdev.Application.DTOs;
using backend_projetdev.Application.UseCases.Conge.Commands;
using backend_projetdev.Application.UseCases.Conge.Commands.YourProject.Application.UseCases.Conge.Commands.UpdateStatusConge;
using backend_projetdev.Application.UseCases.Conge.Queries;
using backend_projetdev.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace backend_projetdev.API.Controllers
{
    [ApiController]
    [Route("conge")]
    [Authorize]
    public class CongeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CongeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCongeDto dto)
        {
            var result = await _mediator.Send(new CreateCongeCommand { Dto = dto });
            return result.Success ? CreatedAtAction(nameof(GetMesConges), new { id = result.Data.Id }, result.Data) : BadRequest(result.Message);
        }

        [HttpGet("mes-conges")]
        public async Task<IActionResult> GetMesConges()
        {
            var result = await _mediator.Send(new GetMesCongesQuery());
            return result.Success ? Ok(result.Data) : NotFound(result.Message);
        }

        [Authorize(Roles = "Manager,Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAllConges()
        {
            var result = await _mediator.Send(new GetAllCongesQuery());
            return result.Success ? Ok(result.Data) : StatusCode(403, result.Message);
        }

        [Authorize(Roles = "Manager,Admin")]
        [HttpPut("status/{id}")]
        public async Task<IActionResult> ModifierStatusConge(int id, [FromBody] Status NewStatus)
        {
            var result = await _mediator.Send(new UpdateStatusCongeCommand { Id = id, NewStatus = NewStatus});
            return result.Success ? Ok(result.Message) : BadRequest(result.Message);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new DeleteCongeCommand { Id = id });
            return result.Success ? Ok(result.Message) : NotFound(result.Message);
        }
    }
}
