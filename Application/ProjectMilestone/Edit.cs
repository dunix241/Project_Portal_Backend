using Application.Core;
using Application.ProjectMilestone.DTOs;
using AutoMapper;
using MediatR;
using Persistence;

namespace Application.ProjectMilestone
{
    public class Edit
    {
        public class Command : IRequest<Result<Unit>>
        {
            public Guid Id { get; set; }
            public EditProjectMilestoneRequestDto dto { get; set; }
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
                var project = await _context.ProjectMilestones.FindAsync(request.Id);
                if (project == null)
                {
                    return null;
                }

                _mapper.Map(request.dto, project);

                await _context.SaveChangesAsync();

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}
