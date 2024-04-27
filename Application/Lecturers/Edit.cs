using Application.Core;
using Application.Lecturers.DTOs;
using Application.Lecturers.Validation;
using AutoMapper;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
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
            private readonly UserManager<User> _userManager;

            public Handler(DataContext context, IMapper mapper, UserManager<User> userManager)
            {
                _context = context;
                _mapper = mapper;
                _userManager = userManager;
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

                var success = true;
                using var transaction = _context.Database.BeginTransaction();
                try
                {
                    if (request.Lecturer.Email != lecturer.Email)
                    {
                        var user = await _userManager.FindByEmailAsync(lecturer.Email);
                        success &= (await _userManager.SetEmailAsync(user, lecturer.Email)).Succeeded;
                    }
                    
                    _mapper.Map(request.Lecturer, lecturer);
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

                return success ? Result<Unit>.Success(Unit.Value) : Result<Unit>.Failure("Problem editing lecturer");
            }
        }
    }
}
