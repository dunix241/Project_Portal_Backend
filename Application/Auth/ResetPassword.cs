using API.DTOs.Accounts;
using Application.Core;
using Application.Users;
using Domain;
using Domain.Mail;
using MediatR;
using Microsoft.AspNetCore.Identity;
using MimeKit;
using Persistence;

namespace Application.Auth;

public class ResetPassword
{
    public class Command : IRequest<Result<Unit>>
    {
        public ResetPasswordRequestDTO ResetPasswordRequestDto { get; set; }
    }

    public class Handler : IRequestHandler<Command, Result<Unit>>
    {
        private readonly DataContext _context;
        private readonly IMediator _mediator;
        private readonly UserManager<User> _userManager;

        public Handler(DataContext context, IMediator mediator, UserManager<User> userManager)
        {
            _context = context;
            _mediator = mediator;
            _userManager = userManager;
        }

        public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {
            var success = true;

            var user = await _userManager.FindByEmailAsync(request.ResetPasswordRequestDto.Email);

            if (user == null)
            {
                return Result<Unit>.Failure("User not found");
            }

            using var transaction = _context.Database.BeginTransaction();
            try
            {
                string filePath = Directory.GetCurrentDirectory() + "\\Templates\\reset-password.html";
                string emailTemplate = await File.ReadAllTextAsync(filePath, cancellationToken);

                success &= (await _mediator.Send(new GeneratePasswordAndSendEmail.Command
                {
                    MailData = new MailData
                    {
                        BodyBuilder = new BodyBuilder
                        {
                            HtmlBody = emailTemplate,
                            TextBody =
                                "A request has been received to reset your password for your Project Portal account\nTo login, use the new password below: {Password}"
                        },
                        Subject = "Project Portal Account's Password",
                        ToAddress = user.Email,
                        ToName = user.Name
                    },
                    Func =
                        async password =>
                        {
                            var succeeded = true;
                            succeeded &= (await _userManager.RemovePasswordAsync(user)).Succeeded;
                            succeeded &= (await _userManager.AddPasswordAsync(user, password)).Succeeded;
                            return succeeded;
                        }
                })).IsSuccess;

                if (success)
                {
                    transaction.Commit();
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return success ? Result<Unit>.Success(Unit.Value) : Result<Unit>.Failure("Error resetting password");
        }
    }
}