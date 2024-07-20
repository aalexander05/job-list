using AutoMapper;
using JobListAPI.Extensions;
using JobListAPI.Models;

namespace JobListAPI.AutoMapperProfiles;

public class AllProfiles : Profile
{
    public AllProfiles()
    {
        CreateMap<Job, DTOs.Job>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToDescription()));

        CreateMap<Job, DTOs.JobRequest>().ReverseMap();

    }
}