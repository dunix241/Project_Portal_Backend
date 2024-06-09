using Application.Core;
using Application.Semesters.DTOs;
using AutoMapper;
using Domain.Semester;
using MediatR;
using Persistence;

namespace Application.Semesters;

public class Create
{
    public class Command : IRequest<Result<Semester>>
    {
        public CreateSemesterRequestDto Semester { get; set; }
    }

    public class Handler : IRequestHandler<Command, Result<Semester>>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public Handler(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<Semester>> Handle(Command request, CancellationToken cancellationToken)
        {
             var semester = new Semester();
             _mapper.Map(request.Semester, semester);

             _context.Semesters.Add(semester);
             await _context.SaveChangesAsync();

             return Result<Semester>.Success(semester);
        }
    }
}