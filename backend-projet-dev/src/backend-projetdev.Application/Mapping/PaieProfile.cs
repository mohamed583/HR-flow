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
    public class PaieProfile : Profile
    {
        public PaieProfile()
        {
            CreateMap<Paie, PaieDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.NomComplet, opt => opt.MapFrom(src => src.Personne.Nom + " " + src.Personne.Prenom))
                .ReverseMap();
        }
    }
}
