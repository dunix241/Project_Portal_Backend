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
using File = Domain.File;
using Application.Minio.DTOs;
using Application.ProjectMilestone.DTOs;
using Application.ProjectMilestoneDetail.DTOs;
using Application.ProjectEnrollment.DTOs;
using Application.ProjectEnrollmentMember.DTOs;


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
        CreateFileMaps();
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

        CreateMap<CreateProjectMilestoneRequestDto, Domain.Project.ProjectMilestone>();
        CreateMap<EditProjectMilestoneRequestDto, Domain.Project.ProjectMilestone>();

        CreateMap<CreateProjectMilestoneDetailRequest, Domain.Project.ProjectMilestoneDetails>();
        CreateMap<EditProjectMilestoneDetailRequest, Domain.Project.ProjectMilestoneDetails>();

        CreateMap<CreateProjectEnrollmentRequestDto, Domain.Project.ProjectEnrollment>();
        CreateMap<EditProjectEnrollmentRequestDto, Domain.Project.ProjectEnrollment>();

        CreateMap<CreateProjectEnrollmentMemberRequest, Domain.Project.ProjectEnrollmentMember>();
        //CreateMap<EditProjectEnrollmentRequestDto, Domain.Project.ProjectEnrollmentMember>();


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
    private void CreateFileMaps()
    {
        CreateMap<EditFileRequestDto,File.File >();
        CreateMap<AddFileResponseDto, File.File>().ReverseMap();
        CreateMap<File.File, AddFileResponseDto>().ReverseMap();
        CreateMap<File.File, GetFileResponseDto>();
        CreateMap<AddFileRequestDto, File.File>();
    }


        
}