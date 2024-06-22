using Application.Core;
using Application.Enrollments.DTOs;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Enrollment;

public class Get
{
    public class Query : IRequest<Result<GetEnrollmentDetailResponseDto>>
    {
        public Guid Id { get; set; }
    }

    public class Handler : IRequestHandler<Query, Result<GetEnrollmentDetailResponseDto>>
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        public Handler(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        public async Task<Result<GetEnrollmentDetailResponseDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var enrollmentId = request.Id;
            var enrollment = await _dataContext.Enrollments
           .Include(e => e.ProjectSemester)
               .ThenInclude(ps => ps.Project)
                   .ThenInclude(p => p.School)
           .Include(e => e.ProjectSemester)
               .ThenInclude(ps => ps.Semester)
           .Include(e => e.EnrollmentMembers)
               .ThenInclude(em => em.User)
           .Where(e => e.Id == enrollmentId)
           .Select(e => new GetEnrollmentDetailResponseDto
           {
               EnrollmentId = e.Id,
               Title = e.Title,
               Description = e.Description,
               RegisterDate = e.RegisterDate,
               CanBeForked = e.CanBeForked,
               HeirFortunes = e.HeirFortunes,
               IsPublished = e.IsPublished,
               PublishDate = e.PublishDate,              
               Project = new ProjectDto
               {
                   Id = e.ProjectSemester.Project.Id,
                   Name = e.ProjectSemester.Project.Name,
                   SchoolName = e.ProjectSemester.Project.School.Name
               },
               Semester = new SemesterDto
               {
                   Id = e.ProjectSemester.Semester.Id,
                   Name = e.ProjectSemester.Semester.Name,
                   StartDate = e.ProjectSemester.Semester.StartDate,
                   EndDate = e.ProjectSemester.Semester.EndDate
               },            
           }).FirstOrDefaultAsync();
            ;
            if (enrollment == null)
            {
                return null;
            }

            var payload = _mapper.Map<GetEnrollmentDetailResponseDto>(enrollment);

            return Result<GetEnrollmentDetailResponseDto>.Success(payload);
        }
    }
}