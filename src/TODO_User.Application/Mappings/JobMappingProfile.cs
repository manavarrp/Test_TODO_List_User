using AutoMapper;
using TODO_User.Application.Feature.Commands.CreateJob;
using TODO_User.Application.Feature.Commands.UpdateJob;
using TODO_User.Application.Feature.Queries.GetJobs;
using TODO_User.Domain.Entities.Users;

namespace TODO_User.Application.Mappings
{
    public class JobMappingProfile : Profile
    {
        public JobMappingProfile()
        {
            CreateMap<Job, CreateJobCommand>().ReverseMap();
            CreateMap<Job, UpdateJobCommand>().ReverseMap();
            CreateMap<Job, GetJobsDto>().
                ForMember(x => x.Status, x => x.MapFrom(y => y.State == 1 ? "Resuelto" : "No resuelto"))
               .ReverseMap();
        }
    }
}
