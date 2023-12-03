using Application.Core;
using Application.Semesters.DTOs;
using AutoMapper;
using MediatR;
using Persistence;

namespace Application.Semesters;

public class Edit
{
    public class Command : IRequest<Result<Unit>>
    {
        public Guid Id { get; set; }
        public EditSemesterRequestDto Semester { get; set; }
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
            var semester = _context.Semesters.FindAsync(request.Id);
            if (semester == null) return null;
            await _mapper.Map(request.Semester, semester);
            await _context.SaveChangesAsync();
            return Result<Unit>.Success(Unit.Value);
        }
    }
}