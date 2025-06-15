using backend_projetdev.Application.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backend_projetdev.Application.UseCases.Auth.Commands
{
    public class LogoutCommand : IRequest<Result>
    {
        public string RefreshToken { get; set; }
    }
}
