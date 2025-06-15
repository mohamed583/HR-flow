using backend_projetdev.Application.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backend_projetdev.Application.UseCases.Conge.Commands
{
    public class DeleteCongeCommand : IRequest<Result>
    {
        public int Id { get; set; }
    }
}
