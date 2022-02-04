using AssignmentWebAPI.Models.Domain;
using AssignmentWebAPI.Models.DTO.Franchise;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssignmentWebAPI.Profiles
{
    public class FranchiseProfile : Profile
    {
        public FranchiseProfile()
        {
            CreateMap<Franchise, FranchiseReadDTO>()
                .ForMember(fdto => fdto.Movies, opt => opt
                .MapFrom(f => f.Movies.Select(f => f.Id).ToArray()));
            CreateMap<Franchise, FranchiseEditDTO>().ReverseMap();
            CreateMap<Franchise, FranchiseCreateDTO>().ReverseMap();
        }
    }
}
