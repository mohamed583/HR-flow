using backend_projetdev.Application.Common;
using backend_projetdev.Application.UseCases.Poste.Commands;
using backend_projetdev.Application.UseCases.Poste.Queries;
using backend_projetdev.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend_projetdev.API.Controllers;
[ApiController]
[Route("poste")]
[Authorize]
public class PosteController : ControllerBase
{
    private readonly IMediator _mediator;

    public PosteController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(new GetAllPostesQuery());
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var result = await _mediator.Send(new GetPosteByIdQuery { Id = id });
        return result.Success ? Ok(result) : NotFound(result);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create([FromBody] CreatePosteCommand command)
    {
        var result = await _mediator.Send(command);
        return result.Success ? CreatedAtAction(nameof(Get), new { id = result.Data }, result) : BadRequest(result);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdatePosteCommand command)
    {
        if (id != command.Id) return BadRequest(Result.Failure("Id mismatch"));
        var result = await _mediator.Send(command);
        return result.Success ? NoContent() : NotFound(result);
    }

    [HttpPatch("statut/{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> ModifierStatut(int id, [FromQuery] StatutPoste status)
    {
        var result = await _mediator.Send(new ModifierStatutPosteCommand { Id = id, NouveauStatut = status });
        return result.Success ? NoContent() : NotFound(result);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _mediator.Send(new DeletePosteCommand { Id = id });
        return result.Success ? NoContent() : NotFound(result);
    }

    [HttpGet("{id}/candidatures")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetCandidatures(int id)
    {
        var result = await _mediator.Send(new GetCandidaturesByPosteQuery { PosteId = id });
        return result.Success ? Ok(result) : NotFound(result);
    }

    [HttpPost("{id}/apply")]
    [Authorize(Roles = "Candidat")]
    public async Task<IActionResult> Apply(int id, IFormFile cvFile)
    {
        var command = new ApplyToPosteCommand
        {
            PosteId = id,
            CvFile = cvFile
        };

        var result = await _mediator.Send(command);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}

