using backend_projetdev.Application.DTOs;
using backend_projetdev.Application.Common;
using MediatR;

namespace backend_projetdev.Application.UseCases.Auth.Queries
{
    public class GetMeQuery : IRequest<Result<PersonneDto>>
    {
    }
}
