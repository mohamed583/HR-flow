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
    public class FormationProfile : Profile
    {
        public FormationProfile()
        {
            CreateMap<Formation, FormationDto>()
             .ReverseMap();

        }
    }

}
