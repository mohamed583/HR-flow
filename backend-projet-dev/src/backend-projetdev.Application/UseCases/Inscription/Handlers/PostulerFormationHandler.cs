using backend_projetdev.Application.Common;
using backend_projetdev.Application.Interfaces;
using backend_projetdev.Application.UseCases.Inscription.Commands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backend_projetdev.Application.UseCases.Inscription.Handlers
{
    public class PostulerFormationHandler : IRequestHandler<PostulerFormationCommand, Result>
    {
        private readonly IInscriptionRepository _repo;
        private readonly ICurrentUserService _currentUserService;

        public PostulerFormationHandler(IInscriptionRepository repo, ICurrentUserService currentUserService)
        {
            _repo = repo;
            _currentUserService = currentUserService;
        }

        public async Task<Result> Handle(PostulerFormationCommand request, CancellationToken cancellationToken)
        {
            var EmployeId = await _currentUserService.GetUserIdAsync();
            return await _repo.PostulerAsync(request.FormationId, EmployeId);
        }
    }
}
