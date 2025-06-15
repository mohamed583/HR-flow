using backend_projetdev.Application.Common;
using backend_projetdev.Application.Interfaces;
using backend_projetdev.Application.UseCases.Poste.Commands;
using backend_projetdev.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backend_projetdev.Application.UseCases.Poste.Handlers
{
    public class ApplyToPosteCommandHandler : IRequestHandler<ApplyToPosteCommand, Result<string>>
    {
        private readonly IPosteRepository _posteRepository;
        private readonly ICandidatureRepository _candidatureRepository;
        private readonly ICurrentUserService _currentUserService;
        public ApplyToPosteCommandHandler(IPosteRepository posteRepository, ICandidatureRepository candidatureRepository, ICurrentUserService currentUserService)
        {
            _posteRepository = posteRepository;
            _currentUserService = currentUserService;
            _candidatureRepository = candidatureRepository;
           
        }

        public async Task<Result<string>> Handle(ApplyToPosteCommand request, CancellationToken cancellationToken)
        {
            var poste = await _posteRepository.GetByIdAsync(request.PosteId);
            if (poste is null)
                return Result<string>.Failure($"Aucun poste trouvé avec l'ID {request.PosteId}.");

            var hasAlreadyApplied = await _candidatureRepository.HasAlreadyApplied(request.CandidatId, request.PosteId);
            if (hasAlreadyApplied)
                return Result<string>.Failure("Vous avez déjà postulé à ce poste.");
            var userId = await _currentUserService.GetUserIdAsync();

            // 1. Vérifier que le fichier est bien là
            if (request.CvFile == null || request.CvFile.Length == 0)
                return Result<string>.Failure("Le fichier CV est invalide.");

            // 2. Définir le chemin de sauvegarde (exemple : wwwroot/cvs/)
            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "cvs");
            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            // 3. Nom de fichier unique
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(request.CvFile.FileName);
            var filePath = Path.Combine(uploadsFolder, fileName);

            // 4. Sauvegarder le fichier sur le disque
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await request.CvFile.CopyToAsync(stream, cancellationToken);
            }

            // 5. Créer la candidature avec le chemin
            var candidature = new Domain.Entities.Candidature
            {
                Id = Guid.NewGuid().ToString(),
                PosteId = request.PosteId,
                CandidatId = userId,
                Status = Status.EnCours,
                CVPath = $"cvs/{fileName}" // Relatif à wwwroot
            };


            await _candidatureRepository.AddAsync(candidature);
            return Result<string>.SuccessResult("Candidature soumise avec succès.");
        }
    }
}
