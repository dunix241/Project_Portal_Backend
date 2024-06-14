using API.DTOs.Accounts;
using Application.EnrollmentPlans.DTOs;
using Application.EnrollmentPlans.EnrollmentPlanDetails.DTOs;
using Application.Enrollments.DTOs;
using Application.Lecturers.DTOs;
using Application.Minio.DTOs;
using Application.MockDomains.DTOs;
using Application.Projects.DTOs;
using Application.Schools.DTOs;
using Application.Semesters.DTOs;
using Application.Semesters.DTOs.Projects;
using Application.Students.DTOs;
using AutoMapper;
using Domain;
using Domain.Enrollment;
using Domain.EnrollmentPlan;
using Domain.Lecturer;
using Domain.MockDomain;
using Domain.Project;
using Domain.School;
using Domain.Semester;
using Domain.Student;
using File = Domain.File.File;

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
        CreateEnrollmentMaps();
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
        CreateMap<CreateStudentRequestDto, User>();
        CreateMap<CreateStudentRequestDto, RegisterRequestDTO>();
        CreateMap<Student, GetStudentResponseDto>()
            .ForMember(dest => dest.SchoolName, opt => opt.MapFrom(src => src.School.Name))
            .ReverseMap();
        CreateMap<User, GetStudentResponseDto>();
        CreateMap<EditStudentRequestDto, Student>();
        CreateMap<EditStudentRequestDto, User>();

        CreateMap<CreateLecturerRequestDto, Lecturer>();
        CreateMap<CreateLecturerRequestDto, User>();
        CreateMap<CreateLecturerRequestDto, RegisterRequestDTO>();
        CreateMap<Lecturer, GetLecturerResponseDto>()
            .ForMember(dest => dest.SchoolName, opt => opt.MapFrom(src => src.School.Name))
            .ReverseMap();
        CreateMap<User, GetLecturerResponseDto>();
        CreateMap<EditLecturerRequestDto, Lecturer>();
        CreateMap<EditLecturerRequestDto, User>();
    }

    private void CreateFileMaps()
    {
        CreateMap<EditFileRequestDto, File>();
        CreateMap<AddFileResponseDto, File>().ReverseMap();
        CreateMap<File, AddFileResponseDto>().ReverseMap();
        CreateMap<File, GetFileResponseDto>();
        CreateMap<AddFileRequestDto, File>();
        CreateMap<File, FileResponseDto>().ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.FileNameWithExtension));
    }

    private void CreateEnrollmentMaps()
    {
        CreateMap<Domain.Enrollment.Enrollment, ProjectJoinedResponseDto>();
        CreateMap<User, EnrollmentMemberResponseDto>();
        CreateMap<Domain.Enrollment.Enrollment, EnrollmentMemberResponseDto>();
        CreateMap<CreateEnrollmentRequestDto, Domain.Enrollment.Enrollment>();
        CreateMap<Project, ListBasedOnEnrollmentPlanResponseDto.RegistrableProjectResponseDto>();
        CreateMap<EditEnrollmentRequestDto, Domain.Enrollment.Enrollment>();
        CreateMap<EditEnrollmentMemberRequestDto, EnrollmentMember>();
        CreateMap<Domain.Enrollment.Enrollment, GetEnrollmentResponseDto>();

        CreateEnrollmentPlanMaps();
    }

    private void CreateEnrollmentPlanMaps()
    {
        CreateMap<CreateEnrollmentPlanRequestDto, EnrollmentPlan>();
        CreateMap<EditEnrollmentPlanRequestDto, EnrollmentPlan>();
        CreateMap<EnrollmentPlan, EnrollmentPlanResponseDto>();
        CreateMap<CreateEnrollmentPlanDetailsRequestDto, EnrollmentPlanDetails>();
        CreateMap<EnrollmentPlanDetails, EnrollmentPlanDetailsResponseDto>();
    }
}