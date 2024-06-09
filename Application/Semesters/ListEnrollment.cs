using Application.Core;
using Domain.Project;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Project = Domain.Project;

namespace Application.Semesters
{
    public class ListEnrollment
    {
        public class Query : IRequest<Result<List<Project.ProjectEnrollment>>>
        {
            public Guid? SemesterId { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<List<Project.ProjectEnrollment>>>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Result<List<Project.ProjectEnrollment>>> Handle(Query request, CancellationToken cancellationToken)
            {
                List<Project.ProjectEnrollment> enrollments;

                if (request.SemesterId.HasValue)
                {
                    var semester = await _context.Semesters
                        .Include(s => s.ProjectSemesters)
                        .ThenInclude(ps => ps.Project)
                        .FirstOrDefaultAsync(s => s.Id == request.SemesterId, cancellationToken);

                    if (semester == null)
                    {
                        return Result<List<Project.ProjectEnrollment>>.Failure("Semester not found.");
                    }

                    var projectSemesterIds = semester.ProjectSemesters.Select(ps => ps.Id).ToList();

                    enrollments = await _context.ProjectEnrollments
                        .Where(pe => projectSemesterIds.Contains(pe.ProjectSemesterId))
                        .ToListAsync(cancellationToken);
                }
                else
                {
                    enrollments = await _context.ProjectEnrollments
                        .ToListAsync(cancellationToken);
                }

                return Result<List<Project.ProjectEnrollment>>.Success(enrollments);
            }
        }
    }
}
