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
    public class LoginCommand : IRequest<Result<TokenDto>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
