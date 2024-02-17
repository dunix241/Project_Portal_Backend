using Application.Core;
using Application.Semesters.DTOs.Projects;
using AutoMapper;
using MediatR;
using Persistence;

namespace Application.Semesters.Projects;

public class Edit
{
    public class Command : IRequest<Result<Unit>>
    {
        public Guid SemesterId { get; set; }
        public Guid ProjectId { get; set; }
        public SemesterEditProjectRequestDto ProjectSemester { get; set; }
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
            var projectSemester = await _context.ProjectSemesters.FindAsync(request.ProjectId, request.SemesterId);
            if (projectSemester == null) return null;

            _mapper.Map(request.ProjectSemester, projectSemester);
            await _context.SaveChangesAsync();

            return Result<Unit>.Success(Unit.Value);
        }
    }
}