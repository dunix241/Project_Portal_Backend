using Application.Core;
using Application.Students.DTOs;
using Application.Students.Validation;
using AutoMapper;
using Domain;
using Domain.Lecturer;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Persistence;

namespace Application.Students
{
    public class Edit
    {
        public class Command : IRequest<Result<GetStudentResponseDto>>
        {
            public string Id { get; set; }
            public EditStudentRequestDto Student { get; set; }
        }
        public class CommandValidator : AbstractValidator<Edit.Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Student).SetValidator(new StudentEditValidator());
            }
        }

        public class Handler : IRequestHandler<Command, Result<GetStudentResponseDto>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            private readonly UserManager<User> _userManager;
            private readonly IMediator _mediator;

            public Handler(DataContext context, IMapper mapper, UserManager<User> userManager, IMediator mediator)
            {
                _context = context;
                _mapper = mapper;
                _userManager = userManager;
                _mediator = mediator;

            }

            public async Task<Result<GetStudentResponseDto>> Handle(Command request, CancellationToken cancellationToken)
            {
                var validationResult = new CommandValidator().Validate(request);

                if (!validationResult.IsValid)
                {
                    return Result<GetStudentResponseDto>.Failure("Validation failed, Name cannot be empty or  contain numbers nor  special characters.");
                }

                var student = await _context.Students.FindAsync(request.Id);
                if (student == null)
                {
                    return Result<GetStudentResponseDto>.Failure($"Student with ID {request.Id} not found.");
                }

                var success = true;
                using var transaction = _context.Database.BeginTransaction();
                try
                {
                    var user = await _userManager.FindByIdAsync(student.UserId);
                    if (request.Student.Email != user.Email)
                    {
                        success &= (await _userManager.SetEmailAsync(user, request.Student.Email)).Succeeded;
                    }
                    
                    _mapper.Map(request.Student, student);
                    _mapper.Map(request.Student, user);
                    await _userManager.UpdateAsync(user);

                    await transaction.CommitAsync(cancellationToken);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw e;
                }

                var response = await _mediator.Send(new Details.Query { Id = student.UserId });

                return success ? Result<GetStudentResponseDto>.Success(_mapper.Map<GetStudentResponseDto>(response.Value)) : Result<GetStudentResponseDto>.Failure("Problem editing student");
            }
        }
    }
}
