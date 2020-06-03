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
                  .ForPath(dest => dest.cityId, opt => opt.MapFrom(src => src.City.Id))
                  .ForPath(dest => dest.mainField, opt => opt.MapFrom(src => src.Subject.Id));

                cfg.CreateMap<Contact, ContactDto>()
                  .ForMember(dest => dest.companyName, opt => opt.MapFrom(src => src.Company.name))
                .ReverseMap()
                 .ForPath(dest => dest.Company.Id, opt => opt.MapFrom(src => src.companyId))
                 .ForPath(dest => dest.Company.name, opt => opt.MapFrom(src => src.companyName));//

                cfg.CreateMap<CoordinatingJobsForGraduates, CoordinatingJobsForGraduatesDto>()
                  .ForMember(dest => dest.candidateName, opt => opt.MapFrom(src => src.Graduate.lastName+" "+ src.Graduate.firstName))
                  .ForMember(dest => dest.jobSubject, opt => opt.MapFrom(src => src.Job.Subject))
                  .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.JobCoordinationStatus))
                  .ReverseMap() 
                  .ForPath(dest => dest.Graduate.Id, opt => opt.MapFrom(src => src.candidateId))
                  //.ForPath(dest => dest.Graduate.firstName+" "+ dest.Graduate.lastName, opt => opt.MapFrom(src => src.candidateName))
                  .ForPath(dest => dest.Job.Id, opt => opt.MapFrom(src => src.jobId))
                  .ForPath(dest => dest.Job.Subject, opt => opt.MapFrom(src => src.jobSubject))
                  .ForPath(dest => dest.placementStatus, opt => opt.MapFrom(src => src.Status.Id))
                  .ForPath(dest => dest.JobCoordinationStatus, opt => opt.MapFrom(src => src.Status));

                cfg.CreateMap<Expertise, ExpertiseDto>().ReverseMap();

                cfg.CreateMap<Graduate, FullGraduateDto>()
                  .ForMember(dest => dest.Languages, opt => opt.MapFrom(src => src.GraduateLanguages))
                  .ReverseMap()
                  .ForPath(dest => dest.cityId, opt => opt.MapFrom(src => src.City.Id))
                  .ForPath(dest => dest.branchId, opt => opt.MapFrom(src => src.Branch.Id))
                  .ForPath(dest => dest.expertiseId, opt => opt.MapFrom(src => src.Expertise.Id))
                  .ForPath(dest => dest.GraduateLanguages, opt => opt.MapFrom(src => src.Languages));

                cfg.CreateMap<GraduateLanguages, GraduateLanguagesDto>()
                   .ForMember(dest => dest.languageName, opt => opt.MapFrom(src => src.Language.name))
                  .ReverseMap()
                  .ForPath(dest => dest.Language.Id, opt => opt.MapFrom(src => src.languageId))
                  .ForPath(dest => dest.Language.name, opt => opt.MapFrom(src => src.languageName));

                cfg.CreateMap<JobCoordinationStatus, JobCoordinationStatusDto>().ReverseMap();

                cfg.CreateMap<Job, JobDto>()
                  .ForMember(dest => dest.contactName, opt => opt.MapFrom(src => src.Contact.name))
                  .ForMember(dest => dest.companyName, opt => opt.MapFrom(src => src.Contact.Company.name))
                  .ForMember(dest => dest.gettingName, opt => opt.MapFrom(src => src.User.name))
                  .ForMember(dest => dest.handlesName, opt => opt.MapFrom(src => src.User1.name))
                  .ForMember(dest => dest.ReasonForClosing, opt => opt.MapFrom(src => src.ReasonForClosingThePosition))
                  .ReverseMap()
                  .ForPath(dest => dest.Contact.Id, opt => opt.MapFrom(src => src.contactId))
                  .ForPath(dest => dest.Contact.name, opt => opt.MapFrom(src => src.contactName))
                  .ForPath(dest => dest.User.Id, opt => opt.MapFrom(src => src.gettingId))
                  .ForPath(dest => dest.User.name, opt => opt.MapFrom(src => src.gettingName))
                  .ForPath(dest => dest.User1.Id, opt => opt.MapFrom(src => src.handlesId))
                  .ForPath(dest => dest.User1.name, opt => opt.MapFrom(src => src.handlesName))
                  .ForPath(dest => dest.reasonForClosing, opt => opt.MapFrom(src => src.ReasonForClosing.Id))
                  .ForPath(dest => dest.ReasonForClosingThePosition, opt => opt.MapFrom(src => src.ReasonForClosing))
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
