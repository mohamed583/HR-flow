using backend_projetdev.Application.Common;
using backend_projetdev.Domain.Enums;
using MediatR;

namespace backend_projetdev.Application.UseCases.Employe.Commands
{
    public class ChangerStatutCommand : IRequest<Result>
    {
        public StatutEmploi Statut { get; set; }
    }

}
