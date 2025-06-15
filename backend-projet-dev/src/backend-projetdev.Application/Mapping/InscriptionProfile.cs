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
    public class InscriptionProfile : Profile
    {
        public InscriptionProfile()
        {
            CreateMap<Inscription, InscriptionDto>()
                .ForMember(dest => dest.FormationId, opt => opt.MapFrom(src => src.FormationId))
                .ForMember(dest => dest.EmployeId, opt => opt.MapFrom(src => src.EmployeId));

            CreateMap<Formation, FormationDto>();
            CreateMap<Formation, FormationsEtInscriptionsDto>();
        }
        
    }
}
