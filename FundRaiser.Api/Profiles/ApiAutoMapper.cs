using AutoMapper;
using FundRaiser.Common.Dto;
using FundRaiser.Common.Models;

namespace FundRaiser.Api.Profiles
{
    public class ApiAutoMapper : Profile
    {
        public ApiAutoMapper()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, UserPostDto>().ReverseMap();

            CreateMap<UserDto, UserPostDto>().ReverseMap();

            CreateMap<Project, ProjectDto>().ReverseMap();
            CreateMap<Project, ProjectPostDto>().ReverseMap();
            CreateMap<Project, ProjectUpdateDto>().ReverseMap();

            CreateMap<Update, UpdateDto>().ReverseMap();
            CreateMap<Update, UpdatePostDto>().ReverseMap();
            CreateMap<Update, UpdatePatchDto>().ReverseMap();

            CreateMap<Reward, RewardDto>().ReverseMap();
            CreateMap<Reward, RewardPostDto>().ReverseMap(); 
        }
    }
}
