using Application.MockDomains.DTOs;
using AutoMapper;
using Domain.MockDomain;

namespace Application.Core;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMockDomainMaps();
    }

    private void CreateMockDomainMaps()
    {
        CreateMap<EditMockDomainRequestDto, MockDomain>();
        CreateMap<CreateMockDomainRequestDto, MockDomain>();
    }
}