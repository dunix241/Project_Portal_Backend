using System.Data.SqlClient;
using API.DTOs.Accounts;
using Application.Auth;
using Application.Core;
using Application.Lecturers.DTOs;
using Application.Lecturers.Validation;
using Application.Users;
using AutoMapper;
using Domain;
using Domain.Lecturer;
using Domain.Mail;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using Persistence;

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
            private readonly IMediator _mediator;
            private readonly UserManager<User> _userManager;

            public Handler(DataContext context, IMapper mapper, IMediator mediator, UserManager<User> userManager)
            {
                _context = context;
                _mapper = mapper;
                _mediator = mediator;
                _userManager = userManager;
            }

            public class CommandValidator : AbstractValidator<Command>
            {
                public CommandValidator()
                {
                    RuleFor(x => x.Lecturer).SetValidator(new LecturerCreateValidator());
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

                using var transaction = _context.Database.BeginTransaction();
                try
                {
                    _context.Lecturers.Add(lecturer);
                    var success = await _context.SaveChangesAsync() != 0;

                    if (success)
                    {
                        string filePath = Directory.GetCurrentDirectory() + "\\Templates\\initial-password.html";
                        string emailTemplate = await File.ReadAllTextAsync(filePath, cancellationToken);

                        success &= (await _mediator.Send(new GeneratePasswordAndSendEmail.Command
                        {
                            MailData = new MailData
                            {
                                BodyBuilder = new BodyBuilder { HtmlBody = emailTemplate, TextBody = "Welcome to Project Portal\nYou've been added to Project Portal, your gateway to project registration and submission!\nTo get started, use the provided password below to log in and start using Project Portal:\n{Password}" },
                                Subject = "Project Portal Account's Password",
                                ToAddress = lecturer.Email,
                                ToName = lecturer.FirstName
                            },
                            Func =
                                async password =>
                                {
                                    return (await _mediator.Send(new Register.Query
                                    {
                                        RegisterRequestDto = new RegisterRequestDTO
                                        {
                                            Email = lecturer.Email, Name = lecturer.FirstName, Password = password,
                                            Address = ""
                                        }
                                    })) != null;
                                }
                        })).IsSuccess;
                    }

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
