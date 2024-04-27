using Application.Core;
using Application.Students.DTOs;
using Application.Students.Validation;
using AutoMapper;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Persistence;

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
            private readonly UserManager<User> _userManager;

            public Handler(DataContext context, IMapper mapper, UserManager<User> userManager)
            {
                _context = context;
                _mapper = mapper;
                _userManager = userManager;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var validationResult = new CommandValidator().Validate(request);

                if (!validationResult.IsValid)
                {
                    return Result<Unit>.Failure("Validation failed, Name cannot be empty or  contain numbers nor  special characters.");
                }

                var student = await _context.Students.FindAsync(request.Id);
                if (student == null)
                {
                    return Result<Unit>.Failure($"Student with ID {request.Id} not found.");
                }

                var success = true;
                using var transaction = _context.Database.BeginTransaction();
                try
                {
                    if (request.Student.Email != student.Email)
                    {
                        var user = await _userManager.FindByEmailAsync(student.Email);
                        success &= (await _userManager.SetEmailAsync(user, student.Email)).Succeeded;
                    }
                    
                    _mapper.Map(request.Student, student);
                    success &= await _context.SaveChangesAsync() != 0;

                    if (success)
                    {
                        transaction.Commit();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw e;
                }

                return success ? Result<Unit>.Success(Unit.Value) : Result<Unit>.Failure("Problem editing student");
            }
        }
    }
}
