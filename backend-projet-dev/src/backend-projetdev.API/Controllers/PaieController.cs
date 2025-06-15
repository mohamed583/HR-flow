using backend_projetdev.Application.Common;
using backend_projetdev.Application.UseCases.Paie.Commands;
using backend_projetdev.Application.UseCases.Paie.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend_projetdev.API.Controllers
{
    [ApiController]
    [Route("paie")]
    [Authorize]
    public class PaieController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PaieController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // 1. Liste de toutes les paies (Admin seulement)
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllPaiesQuery());

            return result.Success ? Ok(result) : BadRequest(result);
        }

        // 2. Paies de l'utilisateur connecté
        [HttpGet("mes-paies")]
        public async Task<IActionResult> GetMesPaies()
        {
            var result = await _mediator.Send(new GetMesPaiesQuery());

            return result.Success ? Ok(result) : Unauthorized(result);
        }

        // 3. Détails d'une paie
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _mediator.Send(new GetPaieByIdQuery { Id = id });

            return result.Success ? Ok(result) : NotFound(result);
        }

        // 4. Créer une paie (Admin uniquement)
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] CreatePaieCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(Result.Failure("Modèle invalide"));

            var result = await _mediator.Send(command);

            return result.Success ? CreatedAtAction(nameof(GetById), new { id = result.Data.Id }, result) : BadRequest(result);
        }

        // 5. Générer la paie pour tous les employés
        [HttpPost("effectuer-tous-employes")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EffectuerPaiePourTous()
        {
            var result = await _mediator.Send(new EffectuerPaiePourTousCommand());

            return result.Success ? Ok(result) : BadRequest(result);
        }
    }
}