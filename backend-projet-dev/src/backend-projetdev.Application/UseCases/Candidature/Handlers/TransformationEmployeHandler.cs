using AutoMapper;
using backend_projetdev.Application.Common;
using backend_projetdev.Application.DTOs;
using backend_projetdev.Application.Interfaces;
using backend_projetdev.Application.UseCases.Candidature.Commands;
using MediatR;

namespace backend_projetdev.Application.UseCases.Candidature.Handlers
{
    public class TransformationEmployeHandler : IRequestHandler<TransformationEmployeCommand, Result>
    {
        private readonly IEmployeService _employeService;
        private readonly IMapper _mapper;

        public TransformationEmployeHandler(IEmployeService employeService, IMapper mapper)
        {
            _employeService = employeService;
            _mapper = mapper;
        }

        public async Task<Result> Handle(TransformationEmployeCommand request, CancellationToken cancellationToken)
        {
            var dto = request.Data;
            var success = await _employeService.TransformerCandidatEnEmploye(dto);

            return success
                ? Result.SuccessResult("Transformation effectuée avec succès.")
                : Result.Failure("Échec de la transformation du candidat.");
        }
    }
}
