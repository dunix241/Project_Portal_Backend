using Application.Core;
using Application.Enrollments.DTOs;
using AutoMapper;
using DocumentFormat.OpenXml.InkML;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Enrollments
{
    public class GetHistory
    {
        public class Query : IRequest<Result<GetEnrollmentHistoryResponseDto>>
        {
            public Guid ProjectId { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<GetEnrollmentHistoryResponseDto>>
        {
            private readonly DataContext _dataContext;
            private readonly IMapper _mapper;

            public Handler(DataContext dataContext, IMapper mapper)
            {
                _dataContext = dataContext;
                _mapper = mapper;
            }

            public async Task<Result<GetEnrollmentHistoryResponseDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var projectId = request.ProjectId;
                var history = await _dataContext.ProjectSemesters
                    .Include(ps => ps.Semester)
                    .Include(ps => ps.Enrollments)
                        .ThenInclude(e => e.EnrollmentMembers)
                        .ThenInclude(em => em.User)
                    .Where(ps => ps.ProjectId == projectId)
                    .Select(ps => new GetEnrollmentHistoryResponseDto
                    {
                        SemesterId = ps.SemesterId,
                        Name = ps.Semester.Name,
                        StartDate = ps.Semester.StartDate,
                        EndDate = ps.Semester.EndDate,
                        Enrollments = ps.Enrollments.Select(e => new EnrollmentDto
                        {
                            EnrollmentId = e.Id,
                            Title = e.Title,
                            Description = e.Description,
                            RegisterDate = e.RegisterDate,
                            Members = e.EnrollmentMembers.Select(em => new EnrollmentMemberDto
                            {
                                MemberId = em.Id,
                                UserId = em.UserId,
                                IsAccepted = em.IsAccepted,
                                RejectReason = em.RejectReason,
                                CreatedAt = em.CreatedAt,
                                UpdatedAt = em.UpdatedAt,
                            }).ToList()
                        }).ToList()
                    }).FirstOrDefaultAsync();

                if (history == null)
                {
                    return null;
                }

                var payload = _mapper.Map<GetEnrollmentHistoryResponseDto>(history);

                return Result<GetEnrollmentHistoryResponseDto>.Success(payload);
            }
        }
    }
}
