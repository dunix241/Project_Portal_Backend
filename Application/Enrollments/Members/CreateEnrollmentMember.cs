using System.ComponentModel.DataAnnotations;
using Application.Core;
using Application.Enrollments.DTOs;
using Application.Mail;
using Domain;
using Domain.Enrollment;
using Domain.Mail;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MimeKit;
using Persistence;
using SmartFormat;

namespace Application.Enrollments.Members;

public class CreateEnrollmentMember
{
    public class Command : IRequest<Result<Unit>>
    {
        public Guid EnrollmentId { get; set; }
        public CreateEnrollmentMemberRequestDto Payload { get; set; }
    }

    public class Handler : IRequestHandler<Command, Result<Unit>>
    {
        private readonly DataContext _dataContext;
        private readonly UserManager<User> _userManager;
        private readonly IMediator _mediator;
        private readonly IConfiguration _configuration;

        public Handler(DataContext dataContext, UserManager<User> userManager, IMediator mediator, IConfiguration configuration)
        {
            _dataContext = dataContext;
            _userManager = userManager;
            _mediator = mediator;
            _configuration = configuration;
        }

        public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Payload.Email);
            var userExists = user != null;
            if (!userExists)
            {
                return Result<Unit>.Failure($"User with email `{request.Payload.Email}` cannot be found");
            }

            var enrollment = await _dataContext.Enrollments.FindAsync(request.EnrollmentId);
            if (enrollment == null) return null;

            var hasAlreadyEnrolled = await _dataContext.EnrollmentMembers
                    .Where(entity => entity.UserId == user.Id)
                    .Include(entity => entity.Enrollment)
                    .Where(entity =>
                        entity.Enrollment.ProjectId == enrollment.ProjectId &&
                        entity.Enrollment.SemesterId == enrollment.SemesterId)
                    .AnyAsync()
                ;
            if (hasAlreadyEnrolled)
            {
                return Result<Unit>.Failure($"User with email `{request.Payload.Email}` already enrolled in another project");
            }

            var enrollmentMember = new EnrollmentMember
            {
                UserId = user.Id, EnrollmentId = request.EnrollmentId
            };

            _dataContext.EnrollmentMembers.Add(enrollmentMember);

            var succeeded = await _dataContext.SaveChangesAsync() != 0;

            if (succeeded)
            {
                string filePath = Directory.GetCurrentDirectory() + "\\Templates\\invitation.html";
                string emailTemplate = await File.ReadAllTextAsync(filePath, cancellationToken);

                var mailData = new MailData
                {
                    BodyBuilder = new BodyBuilder
                    {
                        HtmlBody = emailTemplate,
                        TextBody =
                            "You are invited to a project\n\nJoin the {EnrollmentTitle} ({ProjectName}) team?\n\n{InvitorName} ({InvitorEmail}) invited you to join the team and start collaborating.\n\nFor more details, follow this link to see the invitation: {Url}"
                    },
                    Subject = "You Have a Pending Enrolment Invitation",
                    ToAddress = request.Payload.Email,
                    ToName = user.FullName
                };

                string url = _configuration["ClientUrl"] ?? "https://project-portal.ddns.net:3000";
                Console.WriteLine(url);
                
                var arg = new EmailInvitationDto
                {
                    EnrollmentTitle = enrollment.Title,
                    ProjectName = (await _dataContext.Projects.FindAsync(enrollment.ProjectId)).Name,
                    InvitorEmail = request.Payload.Email,
                    InvitorName = (await _userManager.FindByEmailAsync(request.Payload.Email)).FullName,
                    Url = $"{url}/{enrollmentMember.Id}"
                };
                
                mailData.BodyBuilder.HtmlBody = Smart.Format(mailData.BodyBuilder.HtmlBody, arg);
                mailData.BodyBuilder.TextBody = Smart.Format(mailData.BodyBuilder.TextBody, arg);

                succeeded &= (await _mediator.Send(new Send.Command
                {
                    MailData = mailData
                })).IsSuccess;
            }
            
            var result = succeeded ? Result<Unit>.Success(Unit.Value) : Result<Unit>.Failure("").SetId(Errors.ERROR_ADDING_ENROLLMENT_MEMBER);
            
            return succeeded ? Result<Unit>.Success(Unit.Value) : Result<Unit>.Failure("");
        }
    }
}