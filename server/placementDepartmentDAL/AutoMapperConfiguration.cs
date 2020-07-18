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

                cfg.CreateMap<Company, CompanyDto>().ReverseMap()
                  .ForMember(dest => dest.City, opt => opt.Ignore())
                  .ForMember(dest => dest.Subject, opt => opt.Ignore())
                  .ForPath(dest => dest.cityId, opt => opt.MapFrom(src => src.City.Id))
                  .ForPath(dest => dest.mainField, opt => opt.MapFrom(src => src.Subject.Id));

                cfg.CreateMap<Contact, ContactDto>()
                  .ForMember(dest => dest.companyName, opt => opt.MapFrom(src => src.Company.name))
                .ReverseMap()
                 .ForMember(dest => dest.Company, opt => opt.Ignore());

                cfg.CreateMap<CoordinatingJobsForGraduates, CoordinatingJobsForGraduatesDto>()
                  .ForMember(dest => dest.candidateName, opt => opt.MapFrom(src => src.Graduate.lastName + " " + src.Graduate.firstName))
                  .ForMember(dest => dest.jobSubject, opt => opt.MapFrom(src => src.Job.Subject))
                  .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.JobCoordinationStatus))
                  .ReverseMap()
                  .ForMember(dest => dest.Graduate, opt => opt.Ignore())
                  .ForMember(dest => dest.Job, opt => opt.Ignore())
                  .ForMember(dest => dest.JobCoordinationStatus, opt => opt.Ignore())
                  .ForPath(dest => dest.placementStatus, opt => opt.MapFrom(src => src.Status.Id));

                cfg.CreateMap<Expertise, ExpertiseDto>().ReverseMap();

                cfg.CreateMap<Graduate, FullGraduateDto>()
                  .ForMember(dest => dest.Languages, opt => opt.MapFrom(src => src.GraduateLanguages))
                  .ReverseMap()
                  .ForMember(dest => dest.Branch, opt => opt.Ignore())
                  .ForMember(dest => dest.City, opt => opt.Ignore())
                  .ForMember(dest => dest.Expertise, opt => opt.Ignore())
                  .ForPath(dest => dest.cityId, opt => opt.MapFrom(src => src.City.Id))
                  .ForPath(dest => dest.branchId, opt => opt.MapFrom(src => src.Branch.Id))
                  .ForPath(dest => dest.expertiseId, opt => opt.MapFrom(src => src.Expertise.Id))
                  .ForPath(dest => dest.GraduateLanguages, opt => opt.MapFrom(src => src.Languages));

                cfg.CreateMap<GraduateLanguages, GraduateLanguagesDto>()
                   .ForMember(dest => dest.languageName, opt => opt.MapFrom(src => src.Language.name))
                  .ReverseMap()
                  .ForMember(dest => dest.Graduate, opt => opt.Ignore())
                  .ForMember(dest => dest.Language, opt => opt.Ignore());

                cfg.CreateMap<JobCoordinationStatus, JobCoordinationStatusDto>().ReverseMap();

                cfg.CreateMap<Job, JobDto>()
                  .ForMember(dest => dest.contactName, opt => opt.MapFrom(src => src.Contact.name))
                  .ForMember(dest => dest.companyName, opt => opt.MapFrom(src => src.Contact.Company.name))
                  .ForMember(dest => dest.companyId, opt => opt.MapFrom(src => src.Contact.companyId))
                  .ForMember(dest => dest.gettingName, opt => opt.MapFrom(src => src.User.name))
                  .ForMember(dest => dest.handlesName, opt => opt.MapFrom(src => src.User1.name))
                  .ForMember(dest => dest.ReasonForClosing, opt => opt.MapFrom(src => src.ReasonForClosingThePosition))
                  .ReverseMap()
                  .ForMember(dest => dest.ReasonForClosingThePosition, opt => opt.Ignore())
                  .ForMember(dest => dest.Subject, opt => opt.Ignore())
                  .ForMember(dest => dest.Contact, opt => opt.Ignore())
                  .ForMember(dest => dest.User, opt => opt.Ignore())
                  .ForMember(dest => dest.User1, opt => opt.Ignore())
                  .ForPath(dest => dest.reasonForClosing, opt => opt.MapFrom(src => src.ReasonForClosing.Id))
                  .ForPath(dest => dest.subjectId, opt => opt.MapFrom(src => src.Subject.Id))
                  ;
                cfg.CreateMap<Language, LanguageDto>().ReverseMap();
                cfg.CreateMap<Permission, PermissionDto>().ReverseMap();
                cfg.CreateMap<ReasonForClosingThePosition, ReasonForClosingThePositionDto>().ReverseMap();

                cfg.CreateMap<Reminder, ReminderDto>().ReverseMap();
                cfg.CreateMap<Subject, SubjectDto>().ReverseMap();
                cfg.CreateMap<User, UserDto>().ReverseMap();
            });

            mapper = config.CreateMapper();
        }

    }
}
