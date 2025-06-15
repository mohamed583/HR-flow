using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using MediatR;
using backend_projetdev.Application.UseCases.Employe.Commands;
using backend_projetdev.Application.UseCases.Employe.Queries;

namespace backend_projetdev.API.Controllers;

[ApiController]
[Route("employe")]
public class EmployeController : ControllerBase
{
    private readonly IMediator _mediator;

    public EmployeController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("equipe/{equipeId}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetByEquipe(int equipeId)
    {
        var result = await _mediator.Send(new GetEmployesByEquipeQuery { EquipeId = equipeId });
        return result.Success ? Ok(result.Data) : NotFound(result.Message);
    }

    [HttpPut("{id}/edit")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Edit(string id, [FromBody] EditEmployeCommand command)
    {
        if (id != command.Data.Id)
            return BadRequest(new { message = "L'ID ne correspond pas." });

        var result = await _mediator.Send(command);
        return result.Success ? Ok(result.Message) : BadRequest(result.Message);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(string id)
    {
        var result = await _mediator.Send(new DeleteEmployeCommand { Id = id });
        return result.Success ? Ok(result.Message) : NotFound(result.Message);
    }

    [HttpGet("details/{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetDetails(string id)
    {
        var result = await _mediator.Send(new GetEmployeDetailsQuery { Id = id });
        return result.Success ? Ok(result.Data) : NotFound(result.Message);
    }

    

    [HttpPut("changer-statut")]
    [Authorize(Roles = "Employe")]
    public async Task<IActionResult> ChangerStatut([FromBody] ChangerStatutCommand command)
    {
        var result = await _mediator.Send(command);
        return result.Success ? Ok(new { success = true }) : BadRequest(result.Message);
    }

    [HttpGet("liste-equipe")]
    [Authorize(Roles = "Employe")]
    public async Task<IActionResult> ListeEquipe()
    {
        var result = await _mediator.Send(new GetEmployesDansEquipeQuery());
        return result.Success ? Ok(result.Data) : BadRequest(result.Message);
    }
}
