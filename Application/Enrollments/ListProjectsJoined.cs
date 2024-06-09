using Application.Core;
using Application.Enrollments.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence;
using ProjectJoinedResponseDto = Application.Enrollments.DTOs.ProjectJoinedResponseDto;

namespace Application.Enrollments;

public class ListProjectsJoined
{
    public class Query : IRequest<Result<ListProjectsJoinedResponseDto>>
    {
        
    }

    public class Handler : IRequestHandler<Query, Result<ListProjectsJoinedResponseDto>>
    {
        private readonly DataContext _dataContext;
        private readonly IUserAccessor _userAccessor;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public Handler(DataContext dataContext, IUserAccessor userAccessor, UserManager<User> userManager, IMapper mapper)
        {
            _dataContext = dataContext;
            _userAccessor = userAccessor;
            _userManager = userManager;
            _mapper = mapper;
        }
        
        public async Task<Result<ListProjectsJoinedResponseDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var userEmail = _userAccessor.GetUser().Email;

            var result = await _dataContext.EnrollmentMembers
                .Include(entity => entity.Enrollment)
                .ThenInclude(entity => entity.ProjectSemester)
                .Where(entity => entity.Email == userEmail && entity.IsAccepted == true)
                .OrderByDescending(entity => entity.UpdatedAt)
                .Select(entity => _mapper.Map<ProjectJoinedResponseDto>(entity.Enrollment))
                .ToListAsync();
            
            return Result<ListProjectsJoinedResponseDto>.Success(new ListProjectsJoinedResponseDto{Enrollments = result});
        }
    }
}