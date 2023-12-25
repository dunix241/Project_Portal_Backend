using Application.Lecturers.DTOs;
using Application.MockDomains.DTOs;
using Application.Schools.DTOs;
using Application.Students.DTOs;
using AutoMapper;
using Domain.Lecturer;
using Domain.MockDomain;
using Domain.School;
using Domain.Student;

namespace Application.Core;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMockDomainMaps();
    }


    private void CreateStudentMaps()
    {
        CreateMap<CreateStudentRequestDto, Student>();
        CreateMap<Student, GetStudentResponseDto>()
                .ForMember(dest => dest.SchoolId, opt => opt.MapFrom(src => src.SchoolId))
                .ForMember(dest => dest.SchoolName, opt => opt.MapFrom(src => src.School.Name))
                .ForMember(dest => dest.SchoolCurrentMilestoneId, opt => opt.MapFrom(src => src.School.CurrentMilestoneId))
                .ReverseMap();

        CreateMap<EditStudentRequestDto, Student>();
    }
    private void CreateLecturerMaps()
    {
        CreateMap<CreateLecturerRequedtDto, Lecturer>();
        CreateMap<Lecturer, GetLecturerResponseDto>()
                .ForMember(dest => dest.SchoolId, opt => opt.MapFrom(src => src.SchoolId))
                .ForMember(dest => dest.SchoolName, opt => opt.MapFrom(src => src.School.Name))
                .ForMember(dest => dest.SchoolCurrentMilestoneId, opt => opt.MapFrom(src => src.School.CurrentMilestoneId))
                .ReverseMap();
        CreateMap<EditLecturerRequestDto, Lecturer>();

    }
    private void CreateSchoolMaps()
    {
        CreateMap<CreateSchoolRequestDto, School>();
        CreateMap<EditSchoolRequestDto, School>();
        CreateMap<GetSchoolResponseDto, School>();
        CreateMap<ListSchoolResponseDto, School>();
    }

    private void CreateMockDomainMaps()
    {
        CreateMap<EditMockDomainRequestDto, MockDomain>();
        CreateMap<CreateMockDomainRequestDto, MockDomain>();
    }
}