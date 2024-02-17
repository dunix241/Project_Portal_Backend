using Application.Core;
using Application.Lecturers.DTOs;
using Application.Lecturers.Validation;
using Application.MockDomains.DTOs;
using Application.Students.Validation;
using AutoMapper;
using Domain.Lecturer;
using Domain.MockDomain;
using Domain.Student;
using FluentValidation;
using MediatR;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Lecturers
{
    public class Create
    {
        public class Command : IRequest<Result<Unit>>
        {
            public CreateLecturerRequedtDto Lecturer { get; set; }
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
                    RuleFor(x => x.Lecturer).SetValidator(new LecturerCreateValdator());
                }
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var validationResult = new CommandValidator().Validate(request);
                if (!validationResult.IsValid)
                {
                    return Result<Unit>.Failure("Validation failed, School ID should not be null, Name cannot be empty or contain numbers nor  special characters.");
                }

                var lecturer = _mapper.Map<Lecturer>(request.Lecturer);
                var school = await _context.Schools.FindAsync(request.Lecturer.SchoolId);

                if (school == null)
                {
                    return Result<Unit>.Failure("School not found.");
                }
                lecturer.School = school;

                _context.Lecturers.Add(lecturer);

                await _context.SaveChangesAsync();

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}
