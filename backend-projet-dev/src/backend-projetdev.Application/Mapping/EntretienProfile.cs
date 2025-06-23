using AutoMapper;
using backend_projetdev.Application.DTOs;
using backend_projetdev.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backend_projetdev.Application.Mapping
{
    public class EntretienProfile : Profile
    {
        public EntretienProfile()
        {
            CreateMap<EntretienCreateDto, Entretien>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Status, opt => opt.Ignore())
                .ForMember(dest => dest.Commentaire, opt => opt.Ignore());

            CreateMap<Entretien, EntretienDto>()
    .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));
            CreateMap<Entretien, EntretienDetailsDto>().ReverseMap();
        }
    }
}