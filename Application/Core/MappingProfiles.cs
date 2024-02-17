using Application.MockDomains.DTOs;
using Application.Projects.DTOs;
using Application.Semesters.DTOs;
using Application.Semesters.DTOs.Projects;
using AutoMapper;
using Domain.MockDomain;
using Domain.Project;
using Domain.Semester;

namespace Application.Core;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMockDomainMaps();
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
}