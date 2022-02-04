using AssignmentWebAPI.Models.Domain;
using AssignmentWebAPI.Models.DTO.Character;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssignmentWebAPI.Profiles
{
    public class CharacterProfile : Profile
    {
        public CharacterProfile()
        {
            
            CreateMap<Character, CharacterReadDTO>()
                .ForMember(adto => adto.Movies, opt => opt
                .MapFrom(a => a.Movies.Select(a => a.Id).ToArray()))
                .ReverseMap();
            CreateMap<Character, CharacterEditDTO>()
                .ReverseMap();
            CreateMap<Character, CharacterCreateDTO>()
                .ReverseMap();

        }
    }
}
