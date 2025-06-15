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
    public class EquipeProfile : Profile
    {
        public EquipeProfile()
        {
            CreateMap<Equipe, EquipeDto>()
                .ForMember(dest => dest.DepartementNom, opt => opt.MapFrom(src => src.Departement.Nom))
                .ForMember(dest => dest.EmployeIds, opt => opt.MapFrom(src => src.Employes.Select(e => e.Id).ToList()));
            CreateMap<Equipe, EquipesOfDepartementDto>()
                .ForMember(dest => dest.EmployeIds, opt => opt.MapFrom(src => src.Employes.Select(e => e.Id).ToList()));
        }
    }
}
