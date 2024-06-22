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
                if(userId == null) { 
                }
                var projectId = request.ProjectId;

                var histories = await _dataContext.ProjectSemesters
                    .Where(ps => ps.ProjectId == projectId)
                    .Select(ps => new GetEnrollmentHistoryResponseDto
                    {
                        SemesterId = ps.SemesterId,
                        Name = ps.Semester.Name,
                        StartDate = ps.Semester.StartDate,
                        EndDate = ps.Semester.EndDate,
                        Enrollments = ps.Enrollments
                            .Where(e => e.OwnerId == userId)
                            .Select(e => new EnrollmentDto
                            {
                                EnrollmentId = e.Id,
                                Title = e.Title,
                                Description = e.Description,
                                RegisterDate = e.RegisterDate,
                                Submissions = _dataContext.Submissions
                                    .Where(s => s.EnrollmentId == e.Id)
                                    .Select(s => new SubmissionDto
                                    {
                                        Id = s.Id,
                                        Status = s.Status,
                                        SubmittedDate = s.SubmittedDate,
                                        DueDate = s.DueDate
                                    }).ToList()
                            }).ToList()
                    }).ToListAsync(cancellationToken);

                if (histories == null || !histories.Any())
                {
                    return Result<List<GetEnrollmentHistoryResponseDto>>.Failure("No enrollment history found for the specified project.");
                }

                return Result<List<GetEnrollmentHistoryResponseDto>>.Success(histories);
            }
        }
    }
}
