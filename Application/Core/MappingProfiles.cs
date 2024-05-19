using Application.Lecturers.DTOs;
using Application.MockDomains.DTOs;
using Application.Projects.DTOs;
using Application.Semesters.DTOs;
using Application.Semesters.DTOs.Projects;
using Application.Schools.DTOs;
using Application.Students.DTOs;
using AutoMapper;
using Domain.Lecturer;
using Domain.MockDomain;
using Domain.School;
using Domain.Student;
using Domain.Project;
using Domain.Semester;

namespace Application.Core;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMockDomainMaps();
        CreateSchoolMaps();
        CreateAcademicMaps();
        CreateProjectMaps();
        CreateSemesterMaps();
    }

    private void CreateMockDomainMaps()
    {
        CreateMap<CreateMockDomainRequestDto, MockDomain>();
        CreateMap<EditMockDomainRequestDto, MockDomain>();
    }

    private void CreateProjectMaps()
    {
        CreateMap<CreateProjectRequestDto, Project>();
        CreateMap<EditProjectRequestDto, Project>();
    }

    private void CreateSemesterMaps()
    {
        CreateMap<CreateSemesterRequestDto, Semester>();
        CreateMap<EditSemesterRequestDto, Semester>();

        CreateMap<SemesterCreateProjectRequestDto, ProjectSemester>();
        CreateMap<SemesterEditProjectRequestDto, ProjectSemester>();
    }
    
    private void CreateSchoolMaps()
    {
        CreateMap<CreateSchoolRequestDto, School>();
        CreateMap<EditSchoolRequestDto, School>();
        CreateMap<GetSchoolResponseDto, School>();
        CreateMap<ListSchoolResponseDto, School>();

    }

    private void CreateAcademicMaps()
    {
        CreateMap<CreateStudentRequestDto, Student>();
        CreateMap<Student, GetStudentResponseDto>()
                .ForMember(dest => dest.SchoolName, opt => opt.MapFrom(src => src.School.Name))
                .ReverseMap();
        CreateMap<EditStudentRequestDto, Student>();

        CreateMap<CreateLecturerRequedtDto, Lecturer>();
        CreateMap<Lecturer, GetLecturerResponseDto>()
                .ForMember(dest => dest.SchoolName, opt => opt.MapFrom(src => src.School.Name))
                .ReverseMap();
        CreateMap<EditLecturerRequestDto, Lecturer>();
    }
        
}