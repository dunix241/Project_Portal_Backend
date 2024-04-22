using Application.Core;
using Application.Lecturers.DTOs;
using Application.Lecturers.Validation;
using AutoMapper;
using FluentValidation;
using MediatR;
using Persistence;

namespace Application.Lecturers
{
    public class Edit
    {
        public class Command : IRequest<Result<Unit>>
        {
            public Guid Id { get; set; }
            public EditLecturerRequestDto Lecturer { get; set; }
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

            public class CommandValidator : AbstractValidator<Command>
            {
                public CommandValidator()
                {
                    RuleFor(x => x.Lecturer).SetValidator(new LecturerEditValidator());
                }
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var validationResult = new CommandValidator().Validate(request);
                if (!validationResult.IsValid)
                {
                    return Result<Unit>.Failure("Validation failed, Name cannot be empty or  contain numbers nor  special characters.");
                }
                var lecturer = await _context.Lecturers.FindAsync(request.Id);
                if (lecturer == null)
                {
                    return null;
                }

                _mapper.Map(request.Lecturer, lecturer);

                await _context.SaveChangesAsync();

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}
