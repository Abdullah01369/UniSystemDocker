using AutoMapper;
using UniSystem.Core.DTOs;
using UniSystem.Core.Models;
using UniSystem.Core.Models.ResearcherModels;

namespace UniSystem.Service
{
    public class DtoMapper : Profile
    {
        public DtoMapper()
        {

            CreateMap<UserAppDto, AppUser>().ReverseMap();

            CreateMap<ResearcherInfoMainDto, Researcher>().ReverseMap();
            CreateMap<GradutedStatusStudent, GradutedDto>().ReverseMap();
            CreateMap<EditCourseDto, Lesson>().ReverseMap();
            CreateMap<AnnouncementDto, Announcement>().ReverseMap();
            CreateMap<InBoxListDto, Message>().ReverseMap();
            CreateMap<Product, ProductDto>().ReverseMap()
                           .ForPath(dest => dest.Category.Name, opt => opt.MapFrom(src => src.CategoryName))
                           .ForPath(dest => dest.Category.Id, opt => opt.MapFrom(src => src.CategoryId));


            CreateMap<AppUser, UserAllInfoDto>()
           .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
           .ForMember(dest => dest.Surname, opt => opt.MapFrom(src => src.Surname))
           .ForMember(dest => dest.No, opt => opt.MapFrom(src => src.No))
           .ForMember(dest => dest.TC, opt => opt.MapFrom(src => src.TC))
           .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
           .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
           .ForMember(dest => dest.PhotoBase64Text, opt => opt.MapFrom(src => src.PhotoBase64Text))
           .ForMember(dest => dest.AddressId, opt => opt.MapFrom(src => src.AddressId))
           .ForMember(dest => dest.AddressDec, opt => opt.MapFrom(src => src.Address.Declaration))
           .ReverseMap();

            CreateMap<Project, ProjectDto>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.AcademicianMail, opt => opt.MapFrom(src => src.AppUser.Email))
            .ForMember(dest => dest.AcademicianNameSurname, opt => opt.MapFrom(src => src.AppUser.Name + " " + src.AppUser.Surname))
            .ForMember(dest => dest.CreadtedDate, opt => opt.MapFrom(src => src.CrearedDate))
            .ForMember(dest => dest.Declaration, opt => opt.MapFrom(src => src.Subject))
            .ReverseMap();



            CreateMap<ProjectStudent, StudentListForProjectDto>()
             .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.AppUser.Name))
               .ForMember(dest => dest.Surname, opt => opt.MapFrom(src => src.AppUser.Surname))
                 .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.AppUser.PhoneNumber))
                   .ForMember(dest => dest.PhotoBase64Text, opt => opt.MapFrom(src => src.AppUser.PhotoBase64Text))
                      .ForMember(dest => dest.No, opt => opt.MapFrom(src => src.AppUser.No))
                        .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.AppUser.Email))
                          .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.AppUser.Id))
                            .ForMember(dest => dest.AddingDate, opt => opt.MapFrom(src => src.AddingDate))
                              .ReverseMap();



            CreateMap<Lesson, CourseDto>()
                .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Department.Name)).ReverseMap();


            CreateMap<Lesson, AddCourseDto>().ReverseMap();
            CreateMap<Department, DepartmentListDto>().ReverseMap();


            CreateMap<ResearcherPublications, ResearcherPublicationDto>()
    .ForMember(dest => dest.PublicationMembers, opt => opt.MapFrom(src => src.PublicationMembers));

            CreateMap<PublicationMember, PublicationMemberDto>();




        }
    }
}
