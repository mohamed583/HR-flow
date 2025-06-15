using backend_projetdev.Application.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backend_projetdev.Application.UseCases.Evaluation.Commands
{
    public class LancerCampagneCommand : IRequest<Result>
    {
        public List<string> EmployeIds { get; set; } = new();
    }
}