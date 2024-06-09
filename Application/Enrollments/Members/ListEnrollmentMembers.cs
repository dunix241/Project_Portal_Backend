using Application.Core;
using Application.Enrollments.DTOs;
using AutoMapper;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Enrollments;

public class ListEnrollmentMembers
{
    public class Query : IRequest<Result<ListEnrollmentMembersResponseDto>>
    {
        public Guid Id { get; set; }
    }

    public class Handler : IRequestHandler<Query, Result<ListEnrollmentMembersResponseDto>>
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public Handler(DataContext dataContext, IMapper mapper, UserManager<User> userManager)
        {
            _dataContext = dataContext;
            _mapper = mapper;
            _userManager = userManager;
        }
        
        public async Task<Result<ListEnrollmentMembersResponseDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var members = await _dataContext.EnrollmentMembers
                    .Where(entity => entity.EnrollmentId == request.Id)
                    .Select(entity => _mapper.Map<EnrollmentMemberResponseDto>(entity))
                    .ToListAsync();
            
            foreach (var member in members)
            {
                _mapper.Map(await _userManager.FindByEmailAsync(member.Email), member);
            }

            return Result<ListEnrollmentMembersResponseDto>.Success(new ListEnrollmentMembersResponseDto
                { EnrollmentMembers = members });
        }
    }
}