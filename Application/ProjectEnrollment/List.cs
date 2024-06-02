using Application.Core;
using Application.ProjectEnrollment.DTOs;
using Application.ProjectMilestone.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ProjectEnrollment
{
    public class List
    {
        public class Query : IRequest<Result<ListProjectEnrollmentResponseDto>>
        {
            public ListProjectEnrollmentRequestDto query { get; set; }
            public PagingParams pagination { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<ListProjectEnrollmentResponseDto>>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Result<ListProjectEnrollmentResponseDto>> Handle(Query request, CancellationToken cancellationToken)
            {

                var name = request.query.Name;
                var schoolId = request.query.SchoolId;
                var semesterId = request.query.SemesterId;

                var query = _context.ProjectEnrollments
                                    .Include(pe => pe.ProjectSemester)
                                    .ThenInclude(ps => ps.Project)
                                    .AsQueryable();

                if (!string.IsNullOrEmpty(name))
                {
                    query = query.Where(x => x.Title.ToLower().Contains(name.ToLower()));
                }
                if (schoolId.HasValue)
                {
                    query = query.Where(x => x.ProjectSemester.Project.SchoolId == schoolId);
                }
                if (semesterId.HasValue)
                {
                    query = query.Where(x => x.ProjectSemester.SemesterId == semesterId);
                }

                var projects = new ListProjectEnrollmentResponseDto();
                await projects.GetItemsAsync(query, request.pagination.PageNumber, request.pagination.PageSize);

                return Result<ListProjectEnrollmentResponseDto>.Success(projects);
            }
        }
    }
}
