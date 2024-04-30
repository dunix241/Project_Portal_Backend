using Application.Core;
using Application.Schools.DTOs;
using AutoMapper;
using MediatR;
using Persistence;

namespace Application.Schools
{
    public class Edit
    {
        public class Command : IRequest<Result<GetSchoolResponseDto>>
        {
            public Guid Id { get; set; }
            public EditSchoolRequestDto School { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<GetSchoolResponseDto>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Result<GetSchoolResponseDto>> Handle(Command request, CancellationToken cancellationToken)
            {
                var school = await _context.Schools.FindAsync(request.Id);
                if (school == null)
                {
                    return Result<GetSchoolResponseDto>.Failure("Not found");
                }

                _mapper.Map(request.School, school);

                await _context.SaveChangesAsync();

                return Result<GetSchoolResponseDto>.Success(new GetSchoolResponseDto { School = school});
            }
        }
    }
}
