using Application.Core;
using Application.Schools.DTOs;
using Application.Students.DTOs;
using Application.Students.Validation;
using AutoMapper;
using FluentValidation;
using MediatR;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Students
{
    public class Edit
    {
        public class Command : IRequest<Result<Unit>>
        {
            public Guid Id { get; set; }
            public EditStudentRequestDto Student { get; set; }
        }
        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Student).SetValidator(new StudentEditValidator());
            }
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
                var validationResult = new CommandValidator().Validate(request);

                if (!validationResult.IsValid)
                {
                    return Result<Unit>.Failure("Validation failed, FirstName cannot be empty or  contain numbers nor  special characters.");
                }

                var student = await _context.Students.FindAsync(request.Id);
                if (student == null)
                {
                    return Result<Unit>.Failure($"Student with ID {request.Id} not found.");
                }

                _mapper.Map(request.Student, student);

                await _context.SaveChangesAsync();

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}
