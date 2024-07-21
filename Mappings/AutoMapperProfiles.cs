using Models.Domains;
using Models.DTOs;
using AutoMapper;

namespace Api.Mappings;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<Activity, ActivityDto>().ReverseMap();
        CreateMap<AddActivityRequestDto, Activity>().ReverseMap();
        CreateMap<UpdateActivityRequestDto, Activity>().ReverseMap();
    }
}
