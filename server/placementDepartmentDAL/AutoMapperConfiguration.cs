using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.Configuration;
using placementDepartmentCOMMON;

namespace placementDepartmentDAL
{
    public class AutoMapperConfiguration
    {
        public static MapperConfiguration config;
        public static IMapper mapper ;


        static AutoMapperConfiguration()
        {

             config = new MapperConfiguration(cfg =>
            {
                //Mapping from entity model to DTO and Reverse Mapping
                cfg.CreateMap<Branch, BranchDto>().ReverseMap();
                cfg.CreateMap<City, CityDto>().ReverseMap();
                cfg.CreateMap<Company, CompanyDto>().ReverseMap();
                 cfg.CreateMap<Contact, ContactDto>()
                  .ForMember(dest => dest.CompanyId, opt => opt.MapFrom(src => src.Company.Id))
                  .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.Company.name))
                .ReverseMap()
                 .ForPath(dest => dest.Company.Id, opt => opt.MapFrom(src => src.CompanyId))
                 .ForPath(dest => dest.Company.name, opt => opt.MapFrom(src => src.CompanyName));
                cfg.CreateMap<Coordinating_jobs_for_graduates, CoordinatingJobsForGraduatesDto>()
                  .ForMember(dest => dest.candidateId, opt => opt.MapFrom(src => src.Graduate.Id))
                  .ForMember(dest => dest.candidateName, opt => opt.MapFrom(src => src.Graduate.firstName + " " + src.Graduate.lastName))
                  .ForMember(dest => dest.jobId, opt => opt.MapFrom(src => src.Job.Id))
                  .ForMember(dest => dest.jobSubject, opt => opt.MapFrom(src => src.Job.subject))
                  .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Job_coordination_status))
                  .ReverseMap()
                  .ForPath(dest => dest.Graduate.Id, opt => opt.MapFrom(src => src.candidateId))
                  //.ForPath(dest => dest.Graduate.firstName+" "+ dest.Graduate.firstName, opt => opt.MapFrom(src => src.candidateName))
                  .ForPath(dest => dest.Job.Id, opt => opt.MapFrom(src => src.jobId))
                  .ForPath(dest => dest.Job.subject, opt => opt.MapFrom(src => src.jobSubject))
                  .ForPath(dest => dest.Job_coordination_status, opt => opt.MapFrom(src => src.Status));
                cfg.CreateMap<Expertise, ExpertiseDto>().ReverseMap();
                cfg.CreateMap<Graduate, GraduateDto>()
                  .ForMember(dest => dest.Languages, opt => opt.MapFrom(src => src.Graduate_languages))
                  .ReverseMap()
                  .ForPath(dest => dest.Graduate_languages, opt => opt.MapFrom(src => src.Languages));
                cfg.CreateMap<Graduate_languages, GraduateLanguagesDto>()
                   .ForMember(dest => dest.languageName, opt => opt.MapFrom(src => src.Language.name))
                  .ReverseMap()
                  .ForPath(dest => dest.Language.name, opt => opt.MapFrom(src => src.languageName));
                cfg.CreateMap<Job_coordination_status, JobCoordinationStatusDto>().ReverseMap();
                cfg.CreateMap<Job, JobDto>()
                  .ForMember(dest => dest.contactId, opt => opt.MapFrom(src => src.Contact.Id))
                  .ForMember(dest => dest.contactName, opt => opt.MapFrom(src => src.Contact.name))
                  .ForMember(dest => dest.userGettingId, opt => opt.MapFrom(src => src.User.Id))
                  .ForMember(dest => dest.userGettingName, opt => opt.MapFrom(src => src.User.name))
                  .ForMember(dest => dest.UserHandlesId, opt => opt.MapFrom(src => src.User1.Id))
                  .ForMember(dest => dest.UserHandlesName, opt => opt.MapFrom(src => src.User1.name))
                  .ForMember(dest => dest.ReasonForClosing, opt => opt.MapFrom(src => src.Reason_for_closing_the_position))
                  .ReverseMap()
                  .ForPath(dest => dest.Contact.Id, opt => opt.MapFrom(src => src.contactId))
                  .ForPath(dest => dest.Contact.name, opt => opt.MapFrom(src => src.contactName))
                  .ForPath(dest => dest.User.Id, opt => opt.MapFrom(src => src.userGettingId))
                  .ForPath(dest => dest.User.name, opt => opt.MapFrom(src => src.userGettingName))
                  .ForPath(dest => dest.User1.Id, opt => opt.MapFrom(src => src.UserHandlesId))
                  .ForPath(dest => dest.User1.name, opt => opt.MapFrom(src => src.UserHandlesName))
                  .ForPath(dest => dest.Reason_for_closing_the_position, opt => opt.MapFrom(src => src.ReasonForClosing));
                cfg.CreateMap<Language, LanguageDto>().ReverseMap();
                cfg.CreateMap<Permission, PermissionDto>().ReverseMap();
                cfg.CreateMap<Reason_for_closing_the_position, ReasonForClosingThePositionDto>().ReverseMap();
                cfg.CreateMap<Reminder, ReminderDto>()
                .ForMember(dest => dest.userId, opt => opt.MapFrom(src => src.User.Id))
                .ReverseMap()
                  .ForPath(dest => dest.User.Id, opt => opt.MapFrom(src => src.userId));
                cfg.CreateMap<subject, SubjectDto>().ReverseMap();
                cfg.CreateMap<User, UserDto>().ReverseMap();
            });

            mapper = config.CreateMapper();
        }

    }
}
