using Application.Core;
using Application.Schools.DTOs;
using AutoMapper;
using Domain.School;
using MediatR;
using Persistence;

namespace Application.Schools
{
    public class Create
    {
        public class Command : IRequest<Result<Unit>>
        {
            public CreateSchoolRequestDto School { get; set; }
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
                var school = new School();
                _mapper.Map(request.School, school);
                _context.Schools.Add(school);
                await _context.SaveChangesAsync();

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}
