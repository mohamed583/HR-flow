using backend_projetdev.Application.Common;
using backend_projetdev.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backend_projetdev.Application.UseCases.Evaluation.Commands
{
    public class CreateEvaluationCommand : IRequest<Result<EvaluationDto>>
    {
        public string EmployeId { get; set; }
        public DateTime DateEvaluation { get; set; }
        public string Description { get; set; }
        public string Objectifs { get; set; }
    }
}