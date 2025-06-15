using backend_projetdev.Application.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backend_projetdev.Application.UseCases.Poste.Commands
{
    public class DeletePosteCommand : IRequest<Result>
    {
        public int Id { get; set; }
    }
}
