using AutoMapper;
using backend_projetdev.Application.DTOs;
using backend_projetdev.Application.UseCases.Candidature.Commands;
using backend_projetdev.Domain.Entities;

namespace backend_projetdev.Application.Mapping
{
    public class CandidatureProfile : Profile
    {
        public CandidatureProfile()
        {
            CreateMap<Candidature, CandidatureDto>().ReverseMap();
            CreateMap<Poste, PosteDto>().ReverseMap();
            CreateMap<Candidat, CandidatDto>().ReverseMap();
            CreateMap<TransformationEmployeDto, Employe>();
        }
    }
}
