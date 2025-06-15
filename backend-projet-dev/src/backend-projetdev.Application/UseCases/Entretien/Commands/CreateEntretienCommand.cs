using backend_projetdev.Application.Common;
using backend_projetdev.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backend_projetdev.Application.UseCases.Entretien.Commands
{
    public class CreateEntretienCommand : IRequest<Result<EntretienDto>>
    {
        public EntretienCreateDto Model { get; set; }

    }
}