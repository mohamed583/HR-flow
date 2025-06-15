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
            CreateMap<Employe, EditEmployeDto>().ReverseMap();
        }
    }
}