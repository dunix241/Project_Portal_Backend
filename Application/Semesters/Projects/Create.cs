using Application.Core;
using Application.Semesters.DTOs.Projects;
using AutoMapper;
using Domain.Semester;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Semesters.Projects;

public class Create
{
    public class Command : IRequest<Result<Unit>>
    {
        public Guid SemesterId { get; set; }
        public SemesterCreateProjectRequestDto ProjectSemester { get; set; }
    }

    public class Handler : IRequestHandler<Command, Result<Unit>>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public Handler(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {
            if (!await _context.Semesters.AnyAsync(semester => semester.Id == request.SemesterId))
            {
                return Result<Unit>.Failure("Semester doesn't exist");
            }
            if (!await _context.Projects.AnyAsync(project => project.Id == request.ProjectSemester.ProjectId)) {
                return Result<Unit>.Failure("Project doesn't exist");
            }

            var projectSemester = new ProjectSemester { SemesterId = request.SemesterId };
            _mapper.Map(request.ProjectSemester, projectSemester);
            await _context.ProjectSemesters.AddAsync(projectSemester);
            return Result<Unit>.Success(Unit.Value);
        }
    }
}