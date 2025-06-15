using backend_projetdev.Application.Common;
using backend_projetdev.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backend_projetdev.Application.UseCases.Auth.Commands
{
    public class ChangeLoginInfoCommand : IRequest<Result>
    {
        public string Id { get; set; }
        public ChangeLoginInfoDto Data { get; set; }
    }
}
