using Application.Core;
using Application.Enrollments.DTOs;
using Application.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Enrollments
{
    public class GetHistory
    {
        public class Query : IRequest<Result<List<GetEnrollmentHistoryResponseDto>>>
        {
            public Guid ProjectId { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<List<GetEnrollmentHistoryResponseDto>>>
        {
            private readonly DataContext _dataContext;
            private readonly IMapper _mapper;
            private readonly IUserAccessor _userAccessor;

            public Handler(DataContext dataContext, IMapper mapper, IUserAccessor userAccessor)
            {
                _dataContext = dataContext;
                _mapper = mapper;
                _userAccessor = userAccessor;
            }

            public async Task<Result<List<GetEnrollmentHistoryResponseDto>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var userId = _userAccessor.GetUser().Id;
                if(userId == null)
                {
                    return null;
                }

                var enrollmentMembers = await _dataContext.EnrollmentMembers
                    .Where(entity => entity.UserId == userId)
                    .Include(entity => entity.Enrollment)
                    .ThenInclude(entity => entity.ProjectSemester)
                    .ThenInclude(entity => entity.Semester)
                    .Where(entity => entity.Enrollment.ProjectId == request.ProjectId)
                    .ToListAsync();

                var results = enrollmentMembers.Select(member => _mapper.Map<GetEnrollmentHistoryResponseDto>(member)).ToList();

                return Result<List<GetEnrollmentHistoryResponseDto>>.Success(results);
            }
        }
    }
}
