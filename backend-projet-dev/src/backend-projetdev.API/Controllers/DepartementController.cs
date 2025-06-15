using backend_projetdev.Application.Common;
using backend_projetdev.Application.UseCases.Departement.Commands;
using backend_projetdev.Application.UseCases.Departement.Queries;
using backend_projetdev.Application.DTOs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend_projetdev.API.Controllers;

[ApiController]
[Route("department")]
[Authorize(Roles = "Admin")]
public class DepartementController : ControllerBase
{
    private readonly IMediator _mediator;

    public DepartementController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetDepartements()
    {
        var result = await _mediator.Send(new GetDepartementsQuery());

        if (!result.Success)
            return NotFound(new { result.Message });

        return Ok(result.Data);
    }

    [HttpPost]
    public async Task<IActionResult> CreateDepartement([FromBody] DepartementDto dto)
    {
        var result = await _mediator.Send(new CreateDepartementCommand { Dto = dto });

        if (!result.Success)
            return BadRequest(new { result.Message });

        return CreatedAtAction(nameof(GetDepartementDetails), new { id = result.Data.Id }, result.Data);
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateDepartement(int id, [FromBody] DepartementDto dto)
    {
        var result = await _mediator.Send(new UpdateDepartementCommand { Id = id, Dto = dto });

        if (!result.Success)
            return BadRequest(new { result.Message });

        return Ok(result.Data);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDepartement(int id)
    {
        var result = await _mediator.Send(new DeleteDepartementCommand { Id = id });

        if (!result.Success)
            return Conflict(new { result.Message });

        return NoContent();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetDepartementDetails(int id)
    {
        var result = await _mediator.Send(new GetDepartementDetailsQuery { Id = id });

        if (!result.Success)
            return NotFound(new { result.Message });

        return Ok(result.Data);
    }
}
