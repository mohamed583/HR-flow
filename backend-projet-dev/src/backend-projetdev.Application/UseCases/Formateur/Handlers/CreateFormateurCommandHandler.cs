using AutoMapper;
using backend_projetdev.Application.Common;
using backend_projetdev.Application.Interfaces;
using backend_projetdev.Application.UseCases.Formateur.Commands;
using backend_projetdev.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backend_projetdev.Application.UseCases.Formateur.Handlers
{
    public class CreateFormateurCommandHandler : IRequestHandler<CreateFormateurCommand, Result<string>>
    {
        private readonly IFormateurRepository _repository;
        private readonly IMapper _mapper;
        private readonly IRoleService _roleService;

        public CreateFormateurCommandHandler(IFormateurRepository repository, IMapper mapper, IRoleService roleService)
        {
            _repository = repository;
            _mapper = mapper;
            _roleService = roleService;
        }

        public async Task<Result<string>> Handle(CreateFormateurCommand request, CancellationToken cancellationToken)
        {
            var formateur = _mapper.Map<Domain.Entities.Formateur>(request.Formateur);

            var createResult = await _repository.CreateAsync(formateur, request.Formateur.Password);
            if (!createResult)
                return Result<string>.Failure("Échec de la création du formateur.");

            await _roleService.AddUserToRoleAsync(formateur.Id, "Formateur");

            return Result<string>.SuccessResult("Formateur créé avec succès.");
        }
    }

}
