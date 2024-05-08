using API.DTOs.Accounts;
using Application.Auth;
using Application.Core;
using Application.Students.DTOs;
using Application.Students.Validation;
using Application.Users;
using AutoMapper;
using Domain;
using Domain.Mail;
using Domain.Student;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using MimeKit;
using Persistence;

namespace Application.Students
{
    public class Create
    {
        public class Command : IRequest<Result<Unit>>
        {
            public CreateStudentRequestDto Student { get; set; }
        }
        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Student).SetValidator(new StudentCreateValidator());
            }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;
            private readonly IMediator _mediator;
            private readonly UserManager<User> _userManager;

            public Handler(DataContext context, IMapper mapper, IMediator mediator, UserManager<User> userManager)
            {
                _context = context;
                _mapper = mapper;
                _mediator = mediator;
                _userManager = userManager;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var validationResult = new CommandValidator().Validate(request);
                if (!validationResult.IsValid)
                {
                    return Result<Unit>.Failure("Validation failed, Name cannot be empty or  contain numbers nor  special characters.");
                }

                var student = _mapper.Map<Student>(request.Student);
                var school = await _context.Schools.FindAsync(request.Student.SchoolId);

                if (school == null)
                {
                    return Result<Unit>.Failure("School not found.");
                }
                student.School = school;

                using var transaction = _context.Database.BeginTransaction();
                try
                {
                    var success = true;

                    string filePath = Directory.GetCurrentDirectory() + "\\Templates\\initial-password.html";
                    string emailTemplate = await File.ReadAllTextAsync(filePath, cancellationToken);

                    success &= (await _mediator.Send(new GeneratePasswordAndSendEmail.Command
                    {
                        MailData = new MailData
                        {
                            BodyBuilder = new BodyBuilder
                            {
                                HtmlBody = emailTemplate,
                                TextBody = "Welcome to Project Portal\nYou've been added to Project Portal, your gateway to project registration and submission!\nTo get started, use the provided password below to log in and start using Project Portal:\n{Password}"
                            },
                            Subject = "Project Portal Account's Password",
                            ToAddress = student.Email,
                            ToName = student.Name
                        },
                        Func =
                            async password =>
                            {
                                var success = await _mediator.Send(new Register.Query
                                {
                                    RegisterRequestDto = new RegisterRequestDTO
                                    {
                                        Email = student.Email, Name = student.Name, Password = password,
                                        Address = ""
                                    }
                                }) != null;

                                if (success)
                                {
                                    success &= (await _mediator.Send(new AddToRole.Command
                                    {
                                        Role = Authorization.Constants.StudentsRole,
                                        UserEmail = request.Student.Email
                                    })).IsSuccess;
                                }
                                
                                return success;
                            }
                    })).IsSuccess;

                    student.UserId = (await _userManager.FindByEmailAsync(student.Email)).Id;
                    _context.Students.Add(student);
                    success &= await _context.SaveChangesAsync() != 0;

                    if (success)
                    {
                        transaction.Commit();
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}
