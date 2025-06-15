using backend_projetdev.Application.Common;
using backend_projetdev.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backend_projetdev.Application.UseCases.Conge.Commands
{
    namespace YourProject.Application.UseCases.Conge.Commands.UpdateStatusConge
    {
        public class UpdateStatusCongeCommand : IRequest<Result>
        {
            public int Id { get; set; }
            public Status NewStatus { get; set; }
        }
    }
}
