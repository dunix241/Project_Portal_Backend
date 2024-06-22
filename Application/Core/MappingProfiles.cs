using API.DTOs.Accounts;
using Application.EnrollmentPlans.DTOs;
using Application.EnrollmentPlans.EnrollmentPlanDetails.DTOs;
using Application.Enrollments.DTOs;
using Application.Enrollments.Submissions;
using Application.Enrollments.Submissions.Comments.DTOs;
using Application.Enrollments.Submissions.DTOs;
using Application.Lecturers.DTOs;
using Application.Minio.DTOs;
using Application.MockDomains.DTOs;
using Application.Projects.DTOs;
using Application.Schools.DTOs;
using Application.Semesters.DTOs;
using Application.Semesters.DTOs.Projects;
using Application.Students.DTOs;
using Application.Users.DTOs;
using AutoMapper;
using Domain;
using Domain.Comment;
using Domain.Enrollment;
using Domain.EnrollmentPlan;
using Domain.Lecturer;
using Domain.MockDomain;
using Domain.Project;
using Domain.School;
using Domain.Semester;
using Domain.Student;
using Domain.Submission;
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
        CreateSubmissionMaps();
        CreateSummsionComment();
        CreateUserMapper();
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
        CreateMap<Project, ListBasedOnEnrollmentPlanResponseDto.RegistrableProjectResponseDto>()
            .ForMember(dest => dest.Registrable, options => options.MapFrom<bool>(source => false))
            .ForMember(dest => dest.Enrollment, options => options.MapFrom<Domain.Enrollment.Enrollment>(source => null))
            ;
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

        CreateMap<Domain.Enrollment.EnrollmentMember, EnrollmentMemberResponseDto>();
            ;
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

    private void CreateSubmissionMaps()
    {
        CreateMap<CreateSubmissionRequestDto, Submission>();
        CreateMap<GetSubmissionResponseDto, Submission>();
        CreateMap<EditSubmissionRequestDto, Submission>();
    }


    public void CreateSummsionComment()
    {
        CreateMap<SubmissionCommentDto, SubmissionComment>().ForMember(dest => dest.UserId, opt => opt.Ignore());
        CreateMap<EditSubmissionCommentRequest, SubmissionComment>();
    }

    public void CreateUserMapper()
    {
        CreateMap<EditUserRequest, User>();
    }
}