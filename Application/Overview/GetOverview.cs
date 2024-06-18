using Application.Core;
using Application.Overview.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Overview
{
    public class GetOverview
    {
        public class Query : IRequest<Result<OverviewResponse>>
        {
        }

        public class Handler : IRequestHandler<Query, Result<OverviewResponse>>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Result<OverviewResponse>> Handle(Query request, CancellationToken cancellationToken)
            {
                var currentDate = DateTime.Now;
                var response = new OverviewResponse();

                var currentSemester = await _context.Semesters
                    .Where(x => x.StartDate <= currentDate && x.EndDate >= currentDate)
                    .FirstOrDefaultAsync(cancellationToken);

                if (currentSemester != null)
                {
                    var currentSemesterEnrollments = await _context.Enrollments
                        .Where(x => x.SemesterId == currentSemester.Id)
                        .ToListAsync(cancellationToken);
                    response.CurrentSemesterEnrollments = currentSemesterEnrollments;

                    var submissionStatuses = await _context.Submissions
                        .Where(s => s.Enrollment.SemesterId == currentSemester.Id)
                        .GroupBy(s => s.Status)
                        .Select(g => new SubmissionStatusCount
                        {
                            Status = g.Key,
                            Count = g.Count()
                        })
                        .ToListAsync(cancellationToken);
                    response.SubmissionStatusCount = submissionStatuses;

                    var currentAvailableProjects = await _context.Projects
                        .Where(p => p.ProjectSemesters.Any(ps => ps.SemesterId == currentSemester.Id))
                        .ToListAsync(cancellationToken);
                    response.CurrentAvaiableProjects = currentAvailableProjects;

                    var recentSemesterEnrollments = await _context.Enrollments
                        .OrderByDescending(x => x.RegisterDate)
                        .Take(8)
                        .ToListAsync(cancellationToken);
                    response.RecentSemesterEnrollments = recentSemesterEnrollments;

                    response.EnrollmentCount = await _context.Enrollments.CountAsync(cancellationToken);
                    response.PublishEnrollmentCount = await _context.Enrollments.CountAsync(e => e.IsPublished, cancellationToken);

                    var totalDaysInSemester = (currentSemester.EndDate - currentSemester.StartDate).TotalDays;
                    var daysPassedInSemester = (currentDate - currentSemester.StartDate).TotalDays;
                    response.SemesterProgress = (daysPassedInSemester / totalDaysInSemester) * 100;

                    response.StudentEnrollThisSemester = currentSemesterEnrollments.Count;
                    var previousSemester = await _context.Semesters
                        .Where(x => x.EndDate < currentSemester.StartDate)
                        .OrderByDescending(x => x.EndDate)
                        .FirstOrDefaultAsync(cancellationToken);

                    if (previousSemester != null)
                    {
                        response.StudentEnrollPreviousSemester = await _context.Enrollments
                            .Where(x => x.SemesterId == previousSemester.Id)
                            .CountAsync(cancellationToken);
                    }
                }

                return Result<OverviewResponse>.Success(response);
            }
        }
    }
}
