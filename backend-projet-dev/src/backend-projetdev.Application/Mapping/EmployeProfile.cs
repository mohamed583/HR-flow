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
    public class EmployeProfile : Profile
    {
        public EmployeProfile()
        {
            CreateMap<Employe, EmployeDto>().ReverseMap();
            CreateMap<Employe, EmployeDetailsDto>()
                        .ForMember(dest => dest.EquipeNom, opt => opt.MapFrom(src => src.Equipe != null ? src.Equipe.Nom : null));
            CreateMap<EditEmployeDto, Employe>()
        .ForMember(dest => dest.Equipe, opt => opt.Ignore()) // ignorer les relations complexes si non nécessaires
        .ForMember(dest => dest.Inscriptions, opt => opt.Ignore())
        .ForMember(dest => dest.Evaluations, opt => opt.Ignore())
        .ForMember(dest => dest.Conges, opt => opt.Ignore());


        }


    }
}