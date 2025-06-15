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
    public class EvaluationProfile : Profile
    {
        public EvaluationProfile()
        {
            CreateMap<Evaluation, EvaluationDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.DateEvaluation, opt => opt.MapFrom(src => src.DateEvaluation))
                .ForMember(dest => dest.CommentairesEmploye, opt => opt.MapFrom(src => src.CommentairesEmploye))
                .ForMember(dest => dest.CommentairesResponsable, opt => opt.MapFrom(src => src.CommentairesResponsable))
                .ForMember(dest => dest.Note, opt => opt.MapFrom(src => src.Note))
                .ForMember(dest => dest.EmployeId, opt => opt.MapFrom(src => src.EmployeId))
                .ForMember(dest => dest.ResponsableId, opt => opt.MapFrom(src => src.ResponsableId))
                .ReverseMap();
        }
    }
}