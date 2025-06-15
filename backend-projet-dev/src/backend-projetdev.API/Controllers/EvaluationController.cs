using backend_projetdev.Application.Common;
using backend_projetdev.Application.UseCases.Evaluation.Commands;
using backend_projetdev.Application.UseCases.Evaluation.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace backend_projetdev.API.Controllers
{
    [ApiController]
    [Route("evaluation")]
    public class EvaluationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EvaluationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // 1. Liste des évaluations
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetEvaluations()
        {
            var result = await _mediator.Send(new GetAllEvaluationsQuery());
            return result.Success ? Ok(result) : StatusCode(403, result);
        }

        // 2. Créer une évaluation
        [HttpPost]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> CreateEvaluation([FromBody] CreateEvaluationCommand command)
        {
            var result = await _mediator.Send(command);
            return result.Success ? CreatedAtAction(nameof(GetEvaluationById), new { id = result.Data.Id }, result) : BadRequest(result);
        }

        // 3. Détails d'une évaluation
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetEvaluationById(int id)
        {
            var result = await _mediator.Send(new GetEvaluationByIdQuery { Id = id });
            return result.Success ? Ok(result) : NotFound(result);
        }

        // 4. Mes évaluations
        [HttpGet("mesEvaluations")]
        [Authorize(Roles = "Employe")]
        public async Task<IActionResult> GetMyEvaluations()
        {
            var result = await _mediator.Send(new GetMyEvaluationsQuery());
            return result.Success ? Ok(result) : Unauthorized(result);
        }

        // 5. Finaliser par Employé
        [HttpPut("finaliserParEmploye/{id}")]
        [Authorize(Roles = "Employe")]
        public async Task<IActionResult> FinaliserParEmploye(int id, [FromBody] FinaliserParEmployeCommand command)
        {
            command.EvaluationId = id;
            var result = await _mediator.Send(command);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        // 6. Finaliser par Manager
        [HttpPut("finaliserParManager/{id}")]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> FinaliserParManager(int id, [FromBody] FinaliserParManagerCommand command)
        {
            command.EvaluationId = id;
            var result = await _mediator.Send(command);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        // 7. Approuver ou rejeter
        [HttpPut("approuver/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Approuver(int id, [FromQuery] bool approuve)
        {
            var result = await _mediator.Send(new ApprouverEvaluationCommand { EvaluationId = id, Approuve = approuve });
            return result.Success ? Ok(result) : BadRequest(result);
        }

        // 8. Supprimer
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteEvaluation(int id)
        {
            var result = await _mediator.Send(new DeleteEvaluationCommand { EvaluationId = id });
            return result.Success ? NoContent() : NotFound(result);
        }

        // 9. Lancer campagne
        [HttpPost("lancerCampagne")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> LancerCampagne()
        {
            var result = await _mediator.Send(new LancerCampagneCommand());
            return result.Success ? Ok(result) : BadRequest(result);
        }
    }
}
