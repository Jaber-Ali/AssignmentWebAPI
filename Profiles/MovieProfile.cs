using AssignmentWebAPI.Models.Domain;
using AssignmentWebAPI.Models.DTO.Movie;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssignmentWebAPI.Profiles
{
    public class MovieProfile : Profile
    {
        public MovieProfile()
        {
            CreateMap<Movie, MovieReadDTO>()
                .ForMember(mdto => mdto.Characters, opt => opt
                .MapFrom(m => m.Characters.Select(m => m.Id).ToArray()))
                .ReverseMap();
            CreateMap<Movie, MovieEditDTO>().ReverseMap();
            CreateMap<Movie, MovieCreateDTO>().ReverseMap();
        }
        
    }
}
