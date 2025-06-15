using backend_projetdev.Application.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backend_projetdev.Application.UseCases.Evaluation.Commands
{
    public class DeleteEvaluationCommand : IRequest<Result>
    {
        public int EvaluationId { get; set; }
    }
}