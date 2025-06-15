using backend_projetdev.Application.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backend_projetdev.Application.UseCases.Formateur.Commands
{
    public class DeleteFormateurCommand : IRequest<Result>
    {
        public string Id { get; set; }
    }
}